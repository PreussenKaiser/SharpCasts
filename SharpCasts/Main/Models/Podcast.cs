using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models;

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

/// <summary>
/// The class that represents the 'author' property in a podcast.
/// </summary>
public class EmailContact
{
    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
}

/// <summary>
/// The class that represents a list of episodes from a podcast.
/// </summary>
public class EpisodeList
{
    /// <summary>
    /// Gets or sets the episodes from a podcast.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Episode> List { get; set; }
}
