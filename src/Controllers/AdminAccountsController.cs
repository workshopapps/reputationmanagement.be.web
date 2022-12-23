using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.Entities;
using src.Models.Auth;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers;

/// <summary>
/// Provides functionality for account creation and login for admin to the /Admin/Auth route.
/// </summary>
[Route("api/Admin/Auth")]
[ApiController]
public class AdminAccountsController : ControllerBase
{
    /// <summary>
    /// Automapper interface used for mapping two or more classes
    /// </summary>
    private readonly IMapper _mapper;
    /// <summary>
    /// Store manages users of type <c>ApplicationUser</c> 
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// configuartion setting for JWT
    /// </summary>
    private readonly IConfigurationSection _jwtSettings;
    private readonly IEmailSender _emailSender;

    private readonly ITokenService _tokenService;
    /// <summary>
    /// constructor <c>AdminAccountController</c> initializes an AdminAccount instance
    /// (<paramref name="userManager"/>,<paramref name="configuration"/>).
    /// </summary>
    public AdminAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender, ITokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _jwtSettings = configuration.GetSection("JwtSettings");
        _emailSender = emailSender;
        _tokenService = tokenService;
    }
    /// <summary>
    /// Endpoint responsible for authenticating and login a lawyer.
    /// Achievable through token generation 
    /// </summary>
    /// <param name="adminLogin">contains login details for an admin</param>
    /// <returns>JWT Token for the lawyer, used for authorization</returns>
    [SwaggerOperation(Summary = "Login for an admin")]
    [HttpPost("Login")]
    public async Task<ActionResult<AuthenticatedResponse>> Login([FromBody, SwaggerRequestBody("Account details payload", Required = true)] UserLoginModel adminLogin)
    {
        var user = await _userManager.FindByEmailAsync(adminLogin.Email);
        if (user is null) { return BadRequest($"User with email {adminLogin.Email} does not exist"); }

        if (await _userManager.IsInRoleAsync(user, "Administrator"))
        {
            if (await _userManager.CheckPasswordAsync(user, adminLogin.Password))
            {
                var claims = await _tokenService.GetClaims(user);
                var token = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5);
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

    #region DeleteUsersAccount
    [SwaggerOperation(Summary = "This allows the Admin to delete a customer user account")]
    [HttpDelete("DeleteCustomerAccount")]
    public async Task<IActionResult> DeleteCustomerAccount(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) { return BadRequest($"User with email {email} does not exist"); }
        if (await _userManager.IsInRoleAsync(user, "Customer"))
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("Customer User Deleted");
            }
        }
        return BadRequest("Invalid Authentication");
    }


    [SwaggerOperation(Summary = "Admin to delete a lawyer account with this endpoint")]
    [HttpDelete("DeleteLawyerAccount")]
    public async Task<IActionResult> DeleteLawyerAccount(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) { return BadRequest($"User with email {email} does not exist"); }
        if (await _userManager.IsInRoleAsync(user, "Lawyer"))
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("Lawyer User Deleted");
            }
        }
        return BadRequest("Invalid Authentication");
    }
    #endregion

}