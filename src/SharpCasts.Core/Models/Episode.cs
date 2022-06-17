using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents an episode in a podcast.
/// </summary>
[XmlRoot("rss")]
public class Episode
{
    /// <summary>
    /// Gets or initializes the episodes unique identifier.
    /// </summary>
    [XmlElement("guid")]
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the episodes title.
    /// </summary>
    [XmlElement("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes when the episode was aired.
    /// </summary>
    [XmlElement("pubDate")]
    public string Date { get; init; }

    /// <summary>
    /// Gets or initializes the episodes audio.
    /// </summary>
    [XmlElement("enclosure")]
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
