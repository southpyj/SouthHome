using Microsoft.EntityFrameworkCore;
using SouthHome.Backend.Entities;

namespace SouthHome.Backend.DatabaseContext
{
    public class PgSqlContext : DbContext, IDatabaseContext
    {
        public PgSqlContext(DbContextOptions<PgSqlContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<BlogPostTag> BlogPostTags => Set<BlogPostTag>();
        public DbSet<PostCategory> PostCategories => Set<PostCategory>();
        public DbSet<PortfolioProject> PortfolioProjects => Set<PortfolioProject>();
        public DbSet<SiteOwner> SiteOwners => Set<SiteOwner>();
        public DbSet<ProtfolioProjectTag> PortfolioProjectTags => Set<ProtfolioProjectTag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BlogPost and BlogPostTag relationship (one-to-many)
            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.BlogPostTags)
                .WithOne()
                .HasForeignKey(bpt => bpt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure unique index for Tag name
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
