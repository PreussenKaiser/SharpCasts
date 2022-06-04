using SharpCasts.Main.Models;

using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Responses;

/// <summary>
/// The class that represents the response from a podcasts query to Podchaser.
/// </summary>
public class PodcastResponse
{
    /// <summary>
    /// Gets or sets the data from the response.
    /// </summary>
    [JsonPropertyName("podcasts")]
    public PodcastData Data { get; init; }
}

/// <summary>
/// The class that represents the data from a podcasts response.
/// </summary>
public class PodcastData
{
    /// <summary>
    /// Gets or sets podcasts received by the query.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Podcast> Podcasts { get; init; }
}
