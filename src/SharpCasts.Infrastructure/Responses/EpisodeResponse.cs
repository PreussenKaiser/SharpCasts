using SharpCasts.Core.Models;
using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// The class that represents a response for an episodes query.
/// </summary>
public class EpisodeResponse
{
    /// <summary>
    /// Gets or initializes the podcast containing the episodes.
    /// </summary>
    [JsonPropertyName("podcast")]
    public Podcast Podcast { get; init; }
}
