using Newtonsoft.Json;

namespace SharpCasts.Main.Models;

/// <summary>
/// The model that represents a podcast.
/// </summary>
public class Podcast
{
    /// <summary>
    /// Gets or sets the podcast's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the podcast's title.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the podcast's description.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
}
