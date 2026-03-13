using SouthHome.Backend.Common.Http;
using SouthHome.Backend.Services;
using SouthHome.Shared.Http.Auth;

namespace SouthHome.Backend.Services
{
    public interface ISeedDataService
    {
        Task<ServiceResponse<bool>> InitializeSiteOwnerAsync(InitSiteOwnerRequest request);
    }
}
