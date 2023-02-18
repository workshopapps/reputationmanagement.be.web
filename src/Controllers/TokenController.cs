using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Auth;
using src.Services;
using System.Security.Claims;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = _userManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await _userManager.FindByEmailAsync(username);

            if (user is not null)
            {
                if (user.RefreshToken != refreshToken)
                    return BadRequest("Invalid client request");
                if (user.RefreshTokenExpiryTime <= DateTime.Now)
                    return BadRequest("Invalid client request");

            }
            else { return BadRequest(); }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
           
            _userManager.UpdateAsync(user);

            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("revoke")]
        [Authorize(Roles = "Customer,Lawyer,Admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> Revoke()
        {
            var userEmail = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
