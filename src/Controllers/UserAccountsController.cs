using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.Entities;
using src.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace src.Controllers
{
    [SwaggerTag("For authorization for Customer")]
    [Route("api/auth")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationSection _jwtSettings;

        public UserAccountsController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }

        [HttpPost("create_account")]
        public async Task<ActionResult> Register([FromBody] src.Models.Dtos.CustomerAccountForCreationDto userModel)
        {
            var user = _mapper.Map<ApplicationUser>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                var badResponse = "";
                if (result.Errors.Any(x => x.Code == "PasswordTooShort"))
                {
                    badResponse = "The password is too short";
                }
                else if (result.Errors.Any(Error => Error.Code == "DuplicateEmail"))
                {
                    badResponse = $"Email: \"{userModel.Email}\" is already taken.";
                }
                else
                {
                    badResponse = result.Errors.FirstOrDefault().Description;
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
                    return Ok(token);
                }
            }
            return Ok();
        }

        [HttpPost("sign_in")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user is null) { return BadRequest($"User with email {userModel.Email} does not exist"); }
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
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
    }
}