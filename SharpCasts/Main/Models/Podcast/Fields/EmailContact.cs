using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast.Fields;

/// <summary>
/// The class that represents the 'author' property in a podcast.
/// </summary>
public class EmailContact
{
    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
