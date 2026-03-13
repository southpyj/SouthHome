using Microsoft.EntityFrameworkCore;
using SouthHome.Backend.Common;
using SouthHome.Backend.Common.Enums;
using SouthHome.Backend.Common.Http;
using SouthHome.Backend.DatabaseContext;
using SouthHome.Backend.Entities;
using SouthHome.Shared.Http.Auth;

namespace SouthHome.Backend.Services
{
    public class SeedDataService(IDatabaseContext context, IAuthService authService) : ISeedDataService
    {
        public async Task<ServiceResponse<bool>> InitializeSiteOwnerAsync(InitSiteOwnerRequest request)
        {
            // Check if SiteOwner already exists (single instance system)
            var existingOwner = await context.SiteOwners
                .FirstOrDefaultAsync();

            if (existingOwner != null)
            {
                return ServiceResponse<bool>.Error("站点所有者已初始化", 400);
            }

            // Hash password with salt
            var (passwordHash, salt) = authService.HashPasswordWithSalt(request.Password);

            // Create new SiteOwner entity
            var siteOwner = SiteOwner.CreateInstance(
                request.NickName,
                request.Name,
                request.Avatar,
                passwordHash,
                salt
            );

            context.SiteOwners.Add(siteOwner);
            await context.SaveChangesAsync();

            return ServiceResponse<bool>.Success(true);
        }
    }
}
