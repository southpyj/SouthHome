using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.Auth
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("nickName")]
        public string? NickName { get; set; }
    }
}
