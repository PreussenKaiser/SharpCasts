using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a podcast.
/// </summary>
public class Podcast
{
    /// <summary>
    /// Gets or initializes the podcast's unique identifier.
    /// </summary>
    [JsonPropertyName("itunes_id")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the podcast's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or sets the podcast's description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the podcast's author.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Gets or sets the podcast's website.
    /// </summary>
    public string Website { get; set; }

    /// <summary>
    /// Gets or initializes the podcast's image url.
    /// </summary>
    [JsonPropertyName("image_url")]
    public string Image { get; init; }

    /// <summary>
    /// Gets or initializes where the podcast's feed is.
    /// </summary>
    [JsonPropertyName("feed_url")]
    public string Feed { get; init; }
}
