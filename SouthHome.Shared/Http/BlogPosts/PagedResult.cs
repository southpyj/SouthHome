using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.BlogPosts
{
    public class PagedResult
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("hasPrevious")]
        public bool HasPrevious => Page > 1;

        [JsonPropertyName("hasNext")]
        public bool HasNext => Page < TotalPages;
    }

    public class PagedResult<T> : PagedResult
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new();
    }
}
