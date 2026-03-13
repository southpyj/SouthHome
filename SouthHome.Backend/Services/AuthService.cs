using Microsoft.EntityFrameworkCore;
using SouthHome.Backend.Common;
using SouthHome.Backend.Common.Enums;
using SouthHome.Backend.Common.Http;
using SouthHome.Backend.DatabaseContext;
using SouthHome.Backend.Entities;
using SouthHome.Shared.Http.Auth;
using System.Security.Cryptography;
using System.Text;

namespace SouthHome.Backend.Services
{
    public class AuthService(IDatabaseContext context) : IAuthService
    {
        public async Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            // Username must be "south"
            if (request.Username != "south")
            {
                return ServiceResponse<LoginResponse>.Error("用户名或密码错误", 401);
            }

            // Get SiteOwner from database (only one site owner expected)
            var siteOwner = await context.SiteOwners
                .FirstOrDefaultAsync();

            if (siteOwner == null)
            {
                return ServiceResponse<LoginResponse>.Error("站点所有者未设置", 401);
            }

            // Verify password
            var inputPasswordHash = HashPassword(request.Password, siteOwner.PasswordSalt);
            if (!inputPasswordHash.SequenceEqual(siteOwner.PasswordHash))
            {
                return ServiceResponse<LoginResponse>.Error("用户名或密码错误", 401);
            }

            // Generate token
            var userToken = UserToken.CreateInstance(siteOwner.Id, Role.South);
            var token = userToken.Token;

            var response = new LoginResponse
            {
                Token = token,
                UserId = siteOwner.Id,
                Role = Role.South.ToString(),
                NickName = siteOwner.NickName
            };

            return ServiceResponse<LoginResponse>.Success(response);
        }

        /// <summary>
        /// Hashes a password with the given salt using HMAC-SHA256.
        /// </summary>
        public (byte[] Hash, byte[] Salt) HashPasswordWithSalt(string password)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);
            return (hash, salt);
        }

        /// <summary>
        /// Hashes a password with the given salt using HMAC-SHA256.
        /// </summary>
        private static byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return hmac.ComputeHash(passwordBytes);
        }

        /// <summary>
        /// Generates a random salt for password hashing.
        /// </summary>
        private static byte[] GenerateSalt()
        {
            var salt = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }
}
