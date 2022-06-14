using SharpCasts.Core.Models;
using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents a response for episodes from the <see href="https://api-docs.podchaser.com/">Podchaser API</see>.
/// </summary>
public class EpisodeResponse
{
    /// <summary>
    /// Gets or initializes the podcast containing the episodes.
    /// </summary>
    [JsonPropertyName("podcast")]
    public Podcast Podcast { get; init; }
}
