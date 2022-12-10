using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.Entities;
using src.Models;
using src.Models.Dtos;
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

    /// <summary>
    /// constructor <c>AdminAccountController</c> initializes an AdminAccount instance
    /// (<paramref name="userManager"/>,<paramref name="configuration"/>).
    /// </summary>
    public AdminAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender)
    {
        _mapper = mapper;
        _userManager = userManager;
        _jwtSettings = configuration.GetSection("JwtSettings");
        _emailSender = emailSender;
    }

    /// <summary>
    /// This endpoint allows for creation of an admin
    /// </summary>
    /// <param name="adminRegisterModel">Lawyer registeration details sent as a request</param>
    /// <returns code="200">if account creation is successful</returns>
    /// <returns code="400">If create account fails</returns>
    [SwaggerOperation(Summary = "Create an admin account")]
    [HttpPost("create_account")]
    public async Task<IActionResult> Register([FromBody, SwaggerRequestBody("Account details payload", Required = true)] LawyerAccountForCreationDto adminRegisterModel)
    {
        string EMAIL_BODY = StringTemplates.AdminAccountTemplate;


        var existingAdmin = await _userManager.FindByEmailAsync(adminRegisterModel.Email);

        if(existingAdmin == null)
        {
            var admin = _mapper.Map<ApplicationUser>(adminRegisterModel);
            var result = await _userManager.CreateAsync(admin, adminRegisterModel.Password);

            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }

            await _userManager.AddToRoleAsync(admin, "Administrator");
            await _emailSender.SendEmailAsync(adminRegisterModel.Email, "Negative Reviews Inquiry", EMAIL_BODY);
            return StatusCode(201);
        }

        return BadRequest("Email already in use");
    }


    /// <summary>
    /// Endpoint responsible for authenticating and login a lawyer.
    /// Achievable through token generation 
    /// </summary>
    /// <param name="adminLogin">contains login details for an admin</param>
    /// <returns>JWT Token for the lawyer, used for authorization</returns>
    [SwaggerOperation(Summary = "Login for an admin")]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody, SwaggerRequestBody("Account details payload", Required = true)] UserLoginModel adminLogin)
    {
        var admin = await _userManager.FindByEmailAsync(adminLogin.Email);

        if (admin != null && await _userManager.CheckPasswordAsync(admin, adminLogin.Password))
            {
                var signingCredentials = GetSigningCredentials();
                var claims = GetClaims(admin);
                var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(token);
            }
        return Unauthorized("Invalid Authentication");
    }



        /// <summary>
        /// Generates digital signing and signature for admin
        /// </summary>
        /// <returns>Digiitally signed key for account login operation</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

    /// <summary>
    /// Generates digital signing and signature for admin
    /// </summary>
    /// <returns>Digiitally signed key for account login operation</returns>
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
}