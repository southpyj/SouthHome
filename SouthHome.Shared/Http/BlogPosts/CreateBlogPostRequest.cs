using SouthHome.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.BlogPosts
{
    public class CreateBlogPostRequest
    {
        [Required]
        [MaxLength(64)]
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50)]
        [JsonPropertyName("author")]
        public string Author { get; set; } = "Nan Sun";

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("editType")]
        public EditType EditType { get; set; } = EditType.Markdown;

        [JsonPropertyName("categoryId")]
        public long CategoryId { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();
    }
}
