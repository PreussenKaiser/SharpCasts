using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast;

/// <summary>
/// The class that represents the response from a podcasts query to Podchaser.
/// </summary>
public class PodcastResponse
{
    /// <summary>
    /// Gets or sets the data from the response.
    /// </summary>
    [JsonPropertyName("podcasts")]
    public PodcastData Data { get; set; }
}
