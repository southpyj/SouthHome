using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SouthHome.Shared.Common.Enums;

namespace SouthHome.Shared.Http.BlogPosts
{
    public class UpdateBlogPostRequest
    {
        [MaxLength(64)]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("editType")]
        public EditType? EditType { get; set; }

        [JsonPropertyName("status")]
        public PostStatus? Status { get; set; }

        [JsonPropertyName("categoryId")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }
    }
}
