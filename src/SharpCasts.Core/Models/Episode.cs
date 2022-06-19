using System.Xml.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents an episode in a podcast.
/// </summary>
[XmlRoot("rss", Namespace = ITUNES)]
public class Episode
{
    /// <summary>
    /// The url to the 'itunes' xml namespace.
    /// </summary>
    private const string ITUNES = "http://www.itunes.com/dtds/podcast-1.0.dtd";

    /// <summary>
    /// Gets or initializes the episodes unique identifier.
    /// </summary>
    /// <remarks>
    /// Ideally this would be a <see cref="Guid"/>, but some feeds have CDATA attributes and I don't know how to parse that.
    /// </remarks>
    [XmlText]
    [XmlElement("guid", Namespace = "")]
    public string Id { get; init; }

    /// <summary>
    /// Gets or initializes the episodes title.
    /// </summary>
    [XmlElement("title", Namespace = "")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes the episodes subtitle.
    /// </summary>
    [XmlElement("subtitle")]
    public string Subtitle { get; init; }

    /// <summary>
    /// Gets or initializes the episodes author.
    /// </summary>
    [XmlElement("author", Namespace = "")]
    public string Author { get; init; }

    /// <summary>
    /// Gets or initializes the episodes image.
    /// </summary>
    [XmlElement("image")]
    public Image Image { get; init; }

    /// <summary>
    /// Gets or initializes when the episode was aired.
    /// </summary>
    [XmlElement("pubDate", Namespace = "")]
    public string Date { get; init; }

    /// <summary>
    /// Gets or initializes the episodes audio.
    /// </summary>
    [XmlElement("enclosure", Namespace = "")]
    public Audio Audio { get; init; }
}

/// <summary>
/// Represents episode audio data.
/// </summary>
public class Audio
{
    /// <summary>
    /// Gets or initializes the audio's url.
    /// </summary>
    [XmlAttribute("url")]
    public string Url { get; init; }
}

/// <summary>
/// Represents the episodes image.
/// </summary>
public class Image
{
    /// <summary>
    /// Gets or initializes a url leading to the image.
    /// </summary>
    [XmlAttribute("href")]
    public string Href { get; init; }
}
