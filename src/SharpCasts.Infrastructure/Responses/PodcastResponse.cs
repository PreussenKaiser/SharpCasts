using SharpCasts.Core.Models;
using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents a podcasts response from from <see href="https://allfeeds.ai/api"/>.
/// </summary>
public class PodcastResponse
{
    /// <summary>
    /// Gets or initializes the podcasts in the response.
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<Podcast> Podcasts { get; set; }
}
