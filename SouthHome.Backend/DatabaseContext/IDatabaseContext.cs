using Microsoft.EntityFrameworkCore;
using SouthHome.Backend.Entities;

namespace SouthHome.Backend.DatabaseContext
{
    public interface IDatabaseContext
    {
        DbSet<BlogPost> BlogPosts { get; }
        DbSet<Tag> Tags { get; }
        DbSet<BlogPostTag> BlogPostTags { get; }
        DbSet<PostCategory> PostCategories { get; }
        DbSet<PortfolioProject> PortfolioProjects { get; }
        DbSet<SiteOwner> SiteOwners { get; }
        DbSet<ProtfolioProjectTag> PortfolioProjectTags { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
