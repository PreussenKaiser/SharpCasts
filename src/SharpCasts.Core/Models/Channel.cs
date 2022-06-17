using System.Xml.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a podcast channel.
/// </summary>
public class Channel
{
    /// <summary>
    /// Gets or initializes the channel's description.
    /// </summary>
    [XmlElement("description")]
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes a link to the channel's website.
    /// </summary>
    [XmlElement("link")]
    public string Website { get; init; }

    /// <summary>
    /// Gets or initializes the channel's author.
    /// </summary>
    [XmlElement("itunes:author")]
    public string Author { get; init; }

    /// <summary>
    /// Gets or initializes episodes in the channel.
    /// </summary>
    [XmlElement("item")]
    public Episode[] Episodes { get; init; }
}
