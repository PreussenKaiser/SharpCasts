using System.Text.Json.Serialization;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a podcast.
/// </summary>
public class Podcast
{
    /// <summary>
    /// Gets or initializes the unique identifier for the podcast.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the podcast's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes the podcast's description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes a url leading to an image of the podcast.
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; init; }

    /// <summary>
    /// Gets or initializes the podcast's author.
    /// </summary>
    [JsonPropertyName("author")]
    public EmailContact Author { get; init; }

    /// <summary>
    /// Gets or sets the podcast's list of episodes.
    /// </summary>
    [JsonPropertyName("episodes")]
    public EpisodeList Episodes { get; set; } = new EpisodeList();
}

/// <summary>
/// Represents <see cref="Podcast.Author"/>
/// </summary>
public class EmailContact
{
    /// <summary>
    /// Gets or initializes the name of the author.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
}

/// <summary>
/// Represents a list of episodes from a podcast.
/// </summary>
public class EpisodeList
{
    /// <summary>
    /// Gets or sets the episodes from a podcast.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Episode> List { get; set; }
}
