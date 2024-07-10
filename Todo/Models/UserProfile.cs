using System.Text.Json.Serialization;

namespace Todo.Models
{
    public class UserProfile
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
    }
}
