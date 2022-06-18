using SharpCasts.Core.Models;

using System.Xml.Serialization;

namespace SharpCasts.Infrastructure.Responses;

/// <summary>
/// Represents a response for episodes from the <see href="https://api-docs.podchaser.com/">Podchaser API</see>.
/// </summary>
[XmlRoot("rss")]
public class EpisodeResponse
{
    /// <summary>
    /// Gets or initializes channel information.
    /// </summary>
    [XmlElement("channel")]
    public Channel Channel { get; init; }
}
