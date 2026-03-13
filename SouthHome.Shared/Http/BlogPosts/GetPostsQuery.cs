using SouthHome.Shared.Common.Enums;
using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.BlogPosts
{
    public class GetPostsQuery
    {
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("status")]
        public PostStatus? Status { get; set; } = PostStatus.Published;

        [JsonPropertyName("categoryId")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("tagName")]
        public string? TagName { get; set; }

        [JsonPropertyName("keyword")]
        public string? Keyword { get; set; }
    }
}
