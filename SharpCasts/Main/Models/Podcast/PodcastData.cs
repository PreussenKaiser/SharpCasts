using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast;

/// <summary>
/// The class that represents the data from a podcasts response.
/// </summary>
public class PodcastData
{
    /// <summary>
    /// Gets or sets podcasts received by the query.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Podcast> Podcasts { get; set; }
}
