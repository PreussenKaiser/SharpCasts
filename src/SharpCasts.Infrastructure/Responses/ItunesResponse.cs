using SharpCasts.Core.Models;

using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents a podcast by itunes id response from <see href="https://allfeeds.ai/api"/>.
/// </summary>
public class ItunesResponse
{
    /// <summary>
    /// Gets or initializes the podcast in the response.
    /// </summary>
    [JsonPropertyName("results")]
    public Podcast Podcast { get; init; }
}
