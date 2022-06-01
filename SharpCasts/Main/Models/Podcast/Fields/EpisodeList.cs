using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast.Fields;

/// <summary>
/// The class that represents a list of episodes from a podcast.
/// </summary>
public class EpisodeList
{
    /// <summary>
    /// Gets or sets the episodes from a podcast.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Episode> List { get; set; }
}
