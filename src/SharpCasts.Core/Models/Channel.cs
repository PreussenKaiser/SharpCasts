using System.Xml.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a podcast channel.
/// </summary>
public class Channel
{
    /// <summary>
    /// The url to the 'itunes' xml namespace.
    /// </summary>
    private const string ITUNES = "http://www.itunes.com/dtds/podcast-1.0.dtd";

    /// <summary>
    /// Gets or initializes the channel's title.
    /// </summary>
    [XmlElement("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes the channel's author.
    /// </summary>
    [XmlElement("author", Namespace = ITUNES)]
    public string Author { get; init; }

    /// <summary>
    /// Gets or initializes a url o
    /// </summary>
    [XmlElement("image", Namespace = ITUNES)]
    public string Image { get; init; }

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
    /// Gets or initializes episodes in the channel.
    /// </summary>
    [XmlElement("item")]
    public Episode[] Episodes { get; init; }
}
