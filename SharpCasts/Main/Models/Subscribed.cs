namespace SharpCasts.Main.Models;

/// <summary>
/// The model that represents a subscribed podcast.
/// </summary>
public class Subscribed
{
    /// <summary>
    /// Gets or sets the subscribed podcast's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the subscribed podcast.
    /// </summary>
    public Podcast Podcast { get; set; }

    /// <summary>
    /// Gets or sets the user who subscribed to the podcast.
    /// </summary>
    public User User { get; set; }
}
