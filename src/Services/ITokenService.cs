using Google.Apis.Auth;
using src.Entities;
using src.Models.Dtos;
using System.Security.Claims;

namespace src.Services
{
    public interface ITokenService
    {
        double ExpiryInMinutes { get;set;}
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public Task<List<Claim>> GetClaims(ApplicationUser user);
        public Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)

    }
}
