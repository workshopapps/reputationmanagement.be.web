using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Auth;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace src.Controllers
{
    [Route("api/lawyer/auth")]
    [ApiController]
    public class LawyerAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationSection _jwtSettings;
        private readonly ITokenService _tokenService;

        public LawyerAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
            _tokenService = tokenService;
        }

        [HttpPost("create_account")]
        public async Task<ActionResult<AuthenticatedResponse>> Register([FromBody] CustomerAccountForCreationDto userModel)
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
                    return Unauthorized(result.Errors.First().Description);
                }

                return Unauthorized(badResponse);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Lawyer");
                var newuser = await _userManager.FindByEmailAsync(userModel.Email);
                if (newuser != null && await _userManager.CheckPasswordAsync(newuser, userModel.Password))
                {
                    var refreshTokenExpiryInHours = 24;
                    var claims = await _tokenService.GetClaims(user);
                    var token = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddHours(refreshTokenExpiryInHours);
                    await _userManager.UpdateAsync(user);
                    return Ok(new AuthenticatedResponse() { Token = token, RefreshToken = refreshToken });
                }
            }
            return Unauthorized();
        }

        [HttpPost("sign_in")]
        public async Task<ActionResult<AuthenticatedResponse>> Login([FromBody] UserLoginModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user is null) { return BadRequest($"User with email {userModel.Email} does not exist"); }
            var isInRoleResult = _userManager.IsInRoleAsync(user, "Lawyer");
            if (isInRoleResult.Result == true)
            {
                if (await _userManager.CheckPasswordAsync(user, userModel.Password))
                {
                    var claims = await _tokenService.GetClaims(user);
                    var token = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_tokenService.ExpiryInMinutes);
                    await _userManager.UpdateAsync(user);
                    return Ok(new AuthenticatedResponse() { Token = token, RefreshToken = refreshToken });
                }
                else
                {
                    return Unauthorized("Username and password don't match");
                }
            }
            return Unauthorized("Invalid Authentication");
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