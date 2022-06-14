using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents a unique podcast identifier in the <see href="https://api-docs.podchaser.com/">Podchaser API</see>.
/// </summary>
public class PodcastIdentifier
{
    /// <summary>
    /// Gets or initializes the numeric identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// Gets or initializes the identifier's type.
    /// </summary>
    [JsonPropertyName("type")]
    public PodcastIdentifierType Type { get; init; }
}

/// <summary>
/// The enum that contains podcast identifier types.
/// </summary>
public enum PodcastIdentifierType
{
    APPLE_PODCASTS,
    SPOTIFY,
    RSS,
    PODCHASER
}