using System.Text.Json.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// The class that represents a unique podcast identifier.
/// </summary>
public class PodcastIdentifier
{
    /// <summary>
    /// Gets or sets the numeric identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// Gets or sets the identifier's type.
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