using SouthHome.Backend.Common.Http;
using SouthHome.Shared.Http.BlogPosts;
using SouthHome.Backend.Services;

namespace SouthHome.Backend.Services
{
    public interface IBlogPostService
    {
        Task<ServiceResponse<BlogPostDto>> CreateAsync(CreateBlogPostRequest request, long userId);
        Task<ServiceResponse<BlogPostDto>> UpdateAsync(long id, UpdateBlogPostRequest request);
        Task<ServiceResponse<BlogPostDto>> DeleteAsync(long id);
        Task<ServiceResponse<BlogPostDto>> GetByIdAsync(long id);
        Task<ServiceResponse<PagedResult<BlogPostDto>>> GetPostsAsync(GetPostsQuery query);
        Task<ServiceResponse<List<string>>> GetAllTagsAsync();
    }
}
