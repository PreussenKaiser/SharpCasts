using SharpCasts.Main.Models.Podcast.Fields;
using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast;

/// <summary>
/// The model that represents a podcast.
/// </summary>
public class Podcast
{
    /// <summary>
    /// Gets or sets the unique identifier for the podcast.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the podcast's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or sets the podcast's description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Gets or sets a url leading to an image of the podcast.
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; init; }

    /// <summary>
    /// Gets or sets the podcast's author.
    /// </summary>
    [JsonPropertyName("author")]
    public EmailContact Author { get; init; }

    /// <summary>
    /// Gets or sets the podcast's list of episodes.
    /// </summary>
    [JsonPropertyName("episodes")]
    public EpisodeList Episodes { get; set; } = new EpisodeList();
}
