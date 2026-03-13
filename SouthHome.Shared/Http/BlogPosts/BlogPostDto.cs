using SouthHome.Shared.Common.Enums;
using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.BlogPosts
{
    public class BlogPostDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }

        [JsonPropertyName("updateAt")]
        public DateTime UpdateAt { get; set; }

        [JsonPropertyName("editType")]
        public EditType EditType { get; set; }

        [JsonPropertyName("status")]
        public PostStatus Status { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("categoryId")]
        public long CategoryId { get; set; }
    }
}
