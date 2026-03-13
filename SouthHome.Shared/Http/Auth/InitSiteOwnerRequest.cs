using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SouthHome.Shared.Http.Auth
{
    public class InitSiteOwnerRequest
    {
        [Required]
        [JsonPropertyName("nickName")]
        public string NickName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
