using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SouthHome.Backend.Common.Http;
using SouthHome.Shared.Http.BlogPosts;
using SouthHome.Backend.Services;
using System.Security.Claims;
using SouthHome.Shared.Common.Enums;

namespace SouthHome.Backend.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class BlogPostController(IBlogPostService blogPostService) : ControllerBase
    {
        /// <summary>
        /// 获取文章列表（分页）
        /// </summary>
        [HttpGet]
        public async Task<ServiceResponse<PagedResult<BlogPostDto>>> GetPosts([FromQuery] GetPostsQuery query)
        {
            return await blogPostService.GetPostsAsync(query);
        }

        /// <summary>
        /// 获取单篇文章详情
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ServiceResponse<BlogPostDto>> GetPost(long id)
        {
            return await blogPostService.GetByIdAsync(id);
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        [HttpGet("tags")]
        public async Task<ServiceResponse<List<string>>> GetAllTags()
        {
            return await blogPostService.GetAllTagsAsync();
        }

        /// <summary>
        /// 创建新文章（仅 SiteOwner 可用）
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "SiteOwnerOnly")]
        public async Task<ServiceResponse<BlogPostDto>> CreatePost([FromBody] CreateBlogPostRequest request)
        {
            var userId = long.Parse(User.FindFirst("UserId")?.Value ?? "0");
            return await blogPostService.CreateAsync(request, userId);
        }

        /// <summary>
        /// 更新文章（仅 SiteOwner 可用）
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "SiteOwnerOnly")]
        public async Task<ServiceResponse<BlogPostDto>> UpdatePost(long id, [FromBody] UpdateBlogPostRequest request)
        {
            return await blogPostService.UpdateAsync(id, request);
        }

        /// <summary>
        /// 删除文章（软删除，仅 SiteOwner 可用）
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "SiteOwnerOnly")]
        public async Task<ServiceResponse<BlogPostDto>> DeletePost(long id)
        {
            return await blogPostService.DeleteAsync(id);
        }
    }
}
