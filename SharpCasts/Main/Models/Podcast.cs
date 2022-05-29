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
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the podcast's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the podcast's description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a url leading to an image of the podcast.
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}

/// <summary>
/// The class that represents the response from a podcasts query to Podchaser.
/// </summary>
public class PodcastResponse
{
    /// <summary>
    /// Gets or sets the data from the response.
    /// </summary>
    [JsonPropertyName("podcasts")]
    public PodcastData Data { get; set; }
}

/// <summary>
/// The class that represents the data from a podcasts response.
/// </summary>
public class PodcastData
{
    /// <summary>
    /// Gets or sets podcasts received by the query.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Podcast> Podcasts { get; set; }
}
