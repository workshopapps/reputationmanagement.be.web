using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.Entities;
using src.Models;
using src.Models.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace src.Controllers
{
    [Route("api/lawyer/auth")]
    [ApiController]
    public class LawyerAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationSection _jwtSettings;

        public LawyerAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }

        [HttpPost("create_account")]
        public async Task<ActionResult> Register([FromBody] LawyerAccountForCreationDto lawyerAccountCreationModel)
        {
            var user = _mapper.Map<ApplicationUser>(lawyerAccountCreationModel);
            var result = await _userManager.CreateAsync(user, lawyerAccountCreationModel.Password);
            if (!result.Succeeded)
            {
                var badResponse = "";
                if (result.Errors.Any(x => x.Code == "PasswordTooShort"))
                {
                    badResponse = "The password is too short";
                }
                else if (result.Errors.Any(Error => Error.Code == "DuplicateEmail"))
                {
                    badResponse = $"Email: \"{lawyerAccountCreationModel.Email}\" is already taken.";
                }
                else
                {
                    badResponse = result.Errors.FirstOrDefault().Description;
                }
                return BadRequest(badResponse);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Lawyer");
                var newuser = await _userManager.FindByEmailAsync(lawyerAccountCreationModel.Email);
                if (newuser != null && await _userManager.CheckPasswordAsync(newuser, lawyerAccountCreationModel.Password))
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = GetClaims(user);
                    var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(token);
                }
            }
            return Ok();
        }

        [HttpPost("sign_in")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                var signingCredentials = GetSigningCredentials();
                var claims = GetClaims(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(token);
            }
            return Unauthorized("Invalid Authentication");
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

        /// <returns>Use details of the user</returns>
        /// <response code="200">With the details of the signed in user</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Get User details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpPut("details")]
        public async Task<IActionResult> UpdateSignedInLawyerDetails(LawyerDetailsDto newLawyerDetails)
        {
            var lawyerEmail = User.FindFirstValue(ClaimTypes.Name);
            var signedInLawyer = _userManager.FindByEmailAsync(lawyerEmail).Result;

            _mapper.Map(newLawyerDetails, signedInLawyer);

            var updateAllResult = await _userManager.UpdateAsync(signedInLawyer);
            if (updateAllResult.Succeeded)
            {
                var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(signedInLawyer);
                await _userManager.UpdateNormalizedUserNameAsync(signedInLawyer);
            }
            else
            {
                return BadRequest(updateAllResult.Errors?.FirstOrDefault()?.Description);
            }

            return Ok();
        }

        /// <returns>Use details of the user</returns>
        /// <response code="200">With the details of the signed in user</response>
        /// <response code="400">If the authentication was unsuccessful.</response>
        [SwaggerOperation(Summary = "Get User details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpGet("details")]
        public async Task<IActionResult> GetSignedInLawyerDetails()
        {
            var lawyerEmail = User.FindFirstValue(ClaimTypes.Name);
            var signedInLawyer = _userManager.FindByEmailAsync(lawyerEmail).Result;

            var detailsToReturn = _mapper.Map<LawyerDetailsDto>(signedInLawyer);
            return Ok(detailsToReturn);
        }
    }
}