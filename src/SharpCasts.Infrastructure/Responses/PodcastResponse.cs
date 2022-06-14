using SharpCasts.Core.Models;

using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents the response for podcasts from the <see href="https://api-docs.podchaser.com/">Podchaser API</see>.
/// </summary>
public class PodcastResponse
{
    /// <summary>
    /// Gets or initializes the data from the response.
    /// </summary>
    [JsonPropertyName("podcasts")]
    public PodcastData Data { get; init; }
}

/// <summary>
/// Represents the data from a podcasts response.
/// </summary>
public class PodcastData
{
    /// <summary>
    /// Gets or initializes podcasts received by the query.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Podcast> Podcasts { get; init; }
}
