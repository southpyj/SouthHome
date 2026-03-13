using Microsoft.AspNetCore.Mvc;
using SouthHome.Backend.Common.Http;
using SouthHome.Shared.Http.Auth;
using SouthHome.Backend.Services;

namespace SouthHome.Backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        /// <summary>
        /// 登录获取 Token
        /// </summary>
        /// <remarks>
        /// 用户名必须为 "south"，密码为 SiteOwner 表中设置的密码。
        /// </remarks>
        [HttpPost("login")]
        public async Task<ServiceResponse<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            return await authService.LoginAsync(request);
        }
    }
}
