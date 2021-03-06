using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a podcast subscription.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Gets or initializes the subscribed podcast's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the subscribed podcast.
    /// </summary>
    [Required]
    [ForeignKey(nameof(Podcast))]
    public int PodcastId { get; init; }

    /// <summary>
    /// Gets or initializes the user who subscribed to the podcast.
    /// </summary>
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
}
