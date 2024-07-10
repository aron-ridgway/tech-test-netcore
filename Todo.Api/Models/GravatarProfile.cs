using System.Text.Json.Serialization;

namespace Todo.Api.Models;

public class GravatarProfile
{
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;
}

