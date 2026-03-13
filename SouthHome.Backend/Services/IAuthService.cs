using SouthHome.Backend.Common.Http;
using SouthHome.Shared.Http.Auth;
using SouthHome.Backend.Services;

namespace SouthHome.Backend.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request);
        (byte[] Hash, byte[] Salt) HashPasswordWithSalt(string password);
    }
}
