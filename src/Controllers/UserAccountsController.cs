using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.Entities;
using src.Models;
using src.Models.Dtos;
using src.Models.ExampleModels;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace src.Controllers
{
    [SwaggerTag("Used this endpoint to authorize Customer users")]
    [Route("api/auth")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IEmailSender _emailSender;

        public UserAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration,
             IPasswordValidator<ApplicationUser> passwordValidator,
                IPasswordHasher<ApplicationUser> passwordHasher,
            IEmailSender emailSender)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        /// <returns>User's Auth token if successful.</returns>
        /// <response code="200">Returns OK with the raw auth token as the only response.</response>
        /// <response code="400">If the account creation was unsuccessful due to bad input</response>
        [SwaggerOperation(Summary = "Registers a new User.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(400, typeof(BadCustomerAccountForCreationExample))]
        [SwaggerResponseExample(200, typeof(GoodCustomerAccountForCreationExample))]
        [HttpPost("create_account")]
        public async Task<ActionResult> Register([FromBody] CustomerAccountForCreationDto userModel)
        {
            var user = _mapper.Map<ApplicationUser>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                var badResponse = "";
                if (result.Errors.Any(Error => Error.Code == "PasswordTooShort"))
                {
                    badResponse = "The password is too short";
                }
                else if (result.Errors.Any(Error => Error.Code == "DuplicateEmail"))
                {
                    badResponse = $"Email: \"{userModel.Email}\" is already taken.";
                }
                else
                {
                    return BadRequest(result.Errors.First().Description);
                }

                return BadRequest(badResponse);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                var newuser = await _userManager.FindByEmailAsync(userModel.Email);
                if (newuser != null && await _userManager.CheckPasswordAsync(newuser, userModel.Password))
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = GetClaims(user);
                    var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    string email_title = "Support From Repute";
                    string email_body = StringTemplates.AdminAccountTemplate;
                    await _emailSender.SendEmailAsync(userModel.Email, email_title, email_body);
                    return Ok(token);
                }
            }
            return Ok();
        }

        /// <returns>Use bearer auth token</returns>
        /// <response code="200">Returns OK with the raw auth token as the only content.</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Signs in the user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(400, typeof(BadSignInDetailsForCustomer))]
        [SwaggerResponseExample(200, typeof(GoodSignInDetailsForCustomer))]
        [HttpPost("sign_in")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            
            if (user is null) { return BadRequest($"User with email {userModel.Email} does not exist"); }
            if (await _userManager.IsInRoleAsync(user, "Customer"))
            {
                if (await _userManager.CheckPasswordAsync(user, userModel.Password))
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = GetClaims(user);
                    var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Username and password don't match");
                }
            }
            return BadRequest("Invalid Authentication");
        }

        /// <returns>Use bearer auth token</returns>
        /// <response code="200">Returns OK with the raw auth token as the only content.</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Changes password for user that is logged in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpPost("change_password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModelForSignedInUser passwordResetModel)
        {
            //string? userEmail = HttpContext.User.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            var userEmail = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var passwordValidatorResult = await _passwordValidator.ValidateAsync(_userManager, user, passwordResetModel.OldPassword);

            if (passwordValidatorResult.Succeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, passwordResetModel.NewPassword);
                IdentityResult updatePasswordResult = await _userManager.UpdateAsync(user);

                if (updatePasswordResult.Succeeded) { return Ok("Password changed successfully"); }
                else
                {
                    return BadRequest("the new password is faulty");
                }
            }
            else
            {
                foreach (IdentityError error in passwordValidatorResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest();
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.GetSection("ValidIssuer").Value,
            audience: _jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);
            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        [SwaggerOperation(Summary = "send a password reset token to user that is not logged in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dataModel)
        {
            var user = await _userManager.FindByEmailAsync(dataModel.EmailAddress);
            if (user is null)
            {
                return BadRequest("No user with this email exists");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = $"https://repute.hng.tech/password-recovery/change?"+
                   $"token={token}";

           await _emailSender.SendEmailAsync(dataModel.EmailAddress, "Forgot Password", $"Seems you have forgoten your password, to reset your password please use this <a href=\"{link}\">link</a>");

           return Ok("Please check your email for the password reset link");
        }

        [SwaggerOperation(Summary = "reset users password.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("reset-password.")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto datamodel)
        {
            var user = await _userManager.FindByEmailAsync(datamodel.Email);
            if (user is null)
            {
                return BadRequest("No user with this email exists");
            }

            var result = await _userManager.ResetPasswordAsync(user, datamodel.Token, datamodel.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password reset ");
            }
            return BadRequest("Something went Wrong");
        }

        /// <returns>Use details of the user</returns>
        /// <response code="200">With the details of the signed in user</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Get User details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpGet("details")]
        public async Task<IActionResult> GetSignedInUserDetails()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var SignedInUser = _userManager.FindByEmailAsync(userEmail).Result;

            var detailsToReturn = _mapper.Map<UserDetailsDto>(SignedInUser);
            return Ok(detailsToReturn);
        }

        /// <returns>Use details of the user</returns>
        /// <response code="200">With the details of the signed in user</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Get User details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpPut("details")]
        public async Task<IActionResult> UpdateSignedInUserDetails(UserDetailsDto newUserDetails)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var signedInUser = _userManager.FindByEmailAsync(userEmail).Result;

            signedInUser.Email = newUserDetails.Email;
            signedInUser.PhoneNumber = newUserDetails.PhoneNumber;
            signedInUser.UserName = newUserDetails.BusinessEntityName.Replace(" ", "_");
            signedInUser.FullName = newUserDetails.FullName;
            signedInUser.BusinessWebsite = newUserDetails.BusinessWebsite;
            signedInUser.BusinessDescription = newUserDetails.BusinessDescription;

            var updateAllResult = await _userManager.UpdateAsync(signedInUser);

            var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(signedInUser);
            await _userManager.UpdateNormalizedUserNameAsync(signedInUser);

            return Ok();
        }
    }
}