using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagement.Application.Contracts;

namespace JwtTokenProvider
{
    public class TokenProvider : ITokenProvider
    {
        private const int ExpirationMinutes = 30;
        private readonly ILogger<TokenProvider> _logger;
        private readonly TokenProviderOptions options;

        public TokenProvider(ILogger<TokenProvider> logger, TokenProviderOptions options)
        {
            _logger = logger;
            this.options = options;
        }
        public string CreateToken(IdentityUser user, IEnumerable<string> roles)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                                       CreateClaims(user, roles),
                                       CreateSigningCredentials(),
                                       expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT Token created");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken( List<Claim> claims, SigningCredentials credentials,
           DateTime expiration) =>
           new (
               options.ValidIssuer,
               options.ValidAudience,
               claims,
               expires: expiration,
               signingCredentials: credentials
           );
        private List<Claim> CreateClaims(IdentityUser user, IEnumerable<string> roles)
        {

            try
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName??"Unknown"),
                new Claim(ClaimTypes.Email, user.Email ?? "Unknown" ),
            };
                foreach (var userRole in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                return claims;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating claims");
                throw;
            }
        }

        private  SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(options.SymmetricSecurityKey), SecurityAlgorithms.HmacSha256);
        }
    }
}
