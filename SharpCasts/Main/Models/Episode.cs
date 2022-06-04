using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models;

/// <summary>
/// The class that represents an episode in a podcast.
/// </summary>
public class Episode
{
    /// <summary>
    /// Gets or sets the episodes title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or sets the episodes description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Gets or sets when the episode was aired.
    /// </summary>
    [JsonPropertyName("airDate")]
    public string Date { get; init; }

    /// <summary>
    /// Gets or sets the url leading to the episodes audio.
    /// </summary>
    [JsonPropertyName("audioUrl")]
    public string AudioUrl { get; init; }
}
