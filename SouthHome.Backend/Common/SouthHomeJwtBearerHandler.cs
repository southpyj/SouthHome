using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SouthHome.Backend.Common
{
    public class SouthHomeJwtBearerHandler : AuthenticationHandler<JwtBearerOptions>
    {
        public SouthHomeJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, System.Text.Encodings.Web.UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Missing or invalid Authorization header");
            }

            var tokenString = token.Substring("Bearer ".Length).Trim();
            var userToken = UserToken.CreateInstance(tokenString);

            // Check if token is expired
            if (userToken.GetExpireTime() < DateTime.UtcNow)
            {
                return AuthenticateResult.Fail("Token has expired");
            }

            // Create claims principal
            var claims = new List<Claim>
            {
                new Claim("UserId", userToken.UserId.ToString()),
                new Claim("Role", userToken.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString()),
                new Claim(ClaimTypes.Role, userToken.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }
    }
}
