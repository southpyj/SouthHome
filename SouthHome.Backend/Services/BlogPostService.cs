using Microsoft.EntityFrameworkCore;
using SouthHome.Backend.Common.Http;
using SouthHome.Backend.DatabaseContext;
using SouthHome.Backend.Entities;
using SouthHome.Shared.Http.BlogPosts;
using SouthHome.Shared.Common.Enums;

namespace SouthHome.Backend.Services
{
    public class BlogPostService(IDatabaseContext context) : IBlogPostService
    {
        public async Task<ServiceResponse<BlogPostDto>> CreateAsync(CreateBlogPostRequest request, long userId)
        {
            var post = BlogPost.CreateInstance(userId, request.CategoryId, request.Title);
            post.Author = request.Author;
            post.Content = request.Content ?? string.Empty;
            post.EditType = request.EditType;
            post.Status = PostStatus.Published;
            post.UpdateAt = DateTime.UtcNow;

            context.BlogPosts.Add(post);
            await context.SaveChangesAsync();

            // Handle tags
            await ProcessTagsAsync(post, request.Tags);

            // Reload to get the tags
            var createdPost = await context.BlogPosts
                .Include(b => b.BlogPostTags)
                .FirstOrDefaultAsync(b => b.Id == post.Id);

            if (createdPost == null)
                return ServiceResponse<BlogPostDto>.Error("Failed to create post");

            return ServiceResponse<BlogPostDto>.Success(MapToDto(createdPost));
        }

        public async Task<ServiceResponse<BlogPostDto>> UpdateAsync(long id, UpdateBlogPostRequest request)
        {
            var post = await context.BlogPosts
                .Include(b => b.BlogPostTags)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (post == null)
                return ServiceResponse<BlogPostDto>.Error("Post not found", 404);

            if (request.Title != null)
                post.Title = request.Title;
            if (request.Author != null)
                post.Author = request.Author;
            if (request.Content != null)
                post.Content = request.Content;
            if (request.EditType.HasValue)
                post.EditType = request.EditType.Value;
            if (request.Status.HasValue)
                post.Status = request.Status.Value;
            if (request.CategoryId.HasValue)
                post.CategoryId = request.CategoryId.Value;

            post.UpdateAt = DateTime.UtcNow;

            // Handle tags if provided
            if (request.Tags != null)
            {
                await ProcessTagsAsync(post, request.Tags);
            }

            await context.SaveChangesAsync();

            return ServiceResponse<BlogPostDto>.Success(MapToDto(post));
        }

        public async Task<ServiceResponse<BlogPostDto>> DeleteAsync(long id)
        {
            var post = await context.BlogPosts
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (post == null)
                return ServiceResponse<BlogPostDto>.Error("Post not found", 404);

            post.IsDeleted = true;
            post.DeletedAt = DateTime.UtcNow;
            post.UpdateAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return ServiceResponse<BlogPostDto>.Success(MapToDto(post));
        }

        public async Task<ServiceResponse<BlogPostDto>> GetByIdAsync(long id)
        {
            var post = await context.BlogPosts
                .Include(b => b.BlogPostTags)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (post == null)
                return ServiceResponse<BlogPostDto>.Error("Post not found", 404);

            return ServiceResponse<BlogPostDto>.Success(MapToDto(post));
        }

        public async Task<ServiceResponse<PagedResult<BlogPostDto>>> GetPostsAsync(GetPostsQuery query)
        {
            var queryable = context.BlogPosts
                .Include(b => b.BlogPostTags)
                .Where(b => !b.IsDeleted);

            // Filter by status
            if (query.Status.HasValue)
                queryable = queryable.Where(b => b.Status == query.Status.Value);

            // Filter by category
            if (query.CategoryId.HasValue)
                queryable = queryable.Where(b => b.CategoryId == query.CategoryId.Value);

            // Filter by tag name
            if (!string.IsNullOrWhiteSpace(query.TagName))
                queryable = queryable.Where(b => b.BlogPostTags.Any(t => t.TagName == query.TagName));

            // Filter by keyword (title search)
            if (!string.IsNullOrWhiteSpace(query.Keyword))
                queryable = queryable.Where(b => b.Title.Contains(query.Keyword));

            // Count total
            var totalCount = await queryable.CountAsync();

            // Pagination
            var skip = (query.Page - 1) * query.PageSize;
            var posts = await queryable
                .OrderByDescending(b => b.CreateAt)
                .Skip(skip)
                .Take(query.PageSize)
                .ToListAsync();

            var pagedResult = new PagedResult<BlogPostDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize),
                Items = posts.Select(MapToDto).ToList()
            };

            return ServiceResponse<PagedResult<BlogPostDto>>.Success(pagedResult);
        }

        public async Task<ServiceResponse<List<string>>> GetAllTagsAsync()
        {
            var tags = await context.Tags
                .Where(t => !t.IsDeleted)
                .Select(t => t.Name)
                .Distinct()
                .ToListAsync();

            return ServiceResponse<List<string>>.Success(tags);
        }

        private BlogPostDto MapToDto(BlogPost post)
        {
            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.Author,
                Content = post.Content,
                CreateAt = post.CreateAt,
                UpdateAt = post.UpdateAt,
                EditType = post.EditType,
                Status = post.Status,
                CategoryId = post.CategoryId,
                Tags = post.BlogPostTags
                    .Select(t => t.TagName)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .ToList()
            };
        }

        private async Task ProcessTagsAsync(BlogPost post, List<string> tagNames)
        {
            if (tagNames == null || tagNames.Count == 0)
                return;

            // Remove existing tags
            var existingTags = await context.BlogPostTags
                .Where(bpt => bpt.BlogPostId == post.Id)
                .ToListAsync();
            context.BlogPostTags.RemoveRange(existingTags);

            // Add new tags
            foreach (var tagName in tagNames.Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Find or create tag
                var tag = await context.Tags
                    .FirstOrDefaultAsync(t => t.Name == tagName);

                if (tag == null)
                {
                    tag = Tag.CreateInstance(tagName.Trim());
                    context.Tags.Add(tag);
                    await context.SaveChangesAsync();
                }

                // Create blog post tag
                var blogPostTag = BlogPostTag.Create(post.Id, tag);
                context.BlogPostTags.Add(blogPostTag);
            }

            await context.SaveChangesAsync();
        }
    }
}
