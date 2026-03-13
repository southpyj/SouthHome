using Microsoft.AspNetCore.Mvc;
using SouthHome.Backend.Common.Http;
using SouthHome.Shared.Http.Auth;
using SouthHome.Backend.Services;

namespace SouthHome.Backend.Controllers
{
    [ApiController]
    [Route("api/seed")]
    public class SeedDataController(ISeedDataService seedDataService) : ControllerBase
    {
        /// <summary>
        /// 初始化 SiteOwner 数据（仅一次）
        /// </summary>
        /// <remarks>
        /// 固定用户名为 "south"，密码从请求体中获取
        /// </remarks>
        [HttpPost("init-siteowner")]
        public async Task<ServiceResponse<bool>> InitializeSiteOwner([FromBody] InitSiteOwnerRequest request)
        {
            return await seedDataService.InitializeSiteOwnerAsync(request);
        }
    }
}
