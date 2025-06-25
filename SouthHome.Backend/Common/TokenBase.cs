using System.Text;
using System.Text.Json;

namespace SouthHome.Backend.Common
{
    public class TokenBase : ITokenBase
    {
        /// <summary>
        /// token字符串
        /// </summary>
        private string? _token;

        /// <summary>
        /// 过期时间，默认一天
        /// </summary>
        /// <summary>
        /// 过期时间（Unix 时间戳）
        /// </summary>
        protected long ExpireTimeUnix { get; private set; } = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();

        protected Dictionary<string, string> Claims { get; private set; } = new();  // 可携带的信息

        public virtual string Token
        {
            get
            {
                if (_token is not null) return _token;

                JwtPayload payload = new()
                {
                    { "exp", ExpireTimeUnix },
                    { "claims", JsonSerializer.Serialize(Claims) } // 序列化 Claims
                };
                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(Constants.SecretString));
                SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new(new JwtHeader(credentials), payload);
                _token = new JwtSecurityTokenHandler().WriteToken(token);
                return _token!;
            }
            protected set
            {
                _token = value;
                JwtSecurityTokenHandler handler = new();
                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Constants.SecretString));

                TokenValidationParameters validationParameters = new()
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero  // 避免默认 5 分钟的容差时间
                };

                JwtSecurityToken jwtToken = handler.ReadJwtToken(_token);
                try
                {
                    handler.ValidateToken(_token, validationParameters, out var validatedToken);

                    if (jwtToken.Payload.TryGetValue("exp", out var expObj) && long.TryParse(expObj?.ToString(), out var exp))
                        ExpireTimeUnix = exp;

                    // 解析 Claims
                    if (jwtToken.Payload.TryGetValue("claims", out var claimsJson) && claimsJson is string jsonString)
                        Claims = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString) ?? new();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取 Token 过期时间
        /// </summary>
        public DateTime GetExpireTime()
        {
            return DateTimeOffset.FromUnixTimeSeconds(ExpireTimeUnix).UtcDateTime;
        }
    }
}
