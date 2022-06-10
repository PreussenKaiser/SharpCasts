using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpCasts.Core.Models;

/// <summary>
/// The model that represents a subscribed podcast.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Gets or sets the subscribed podcast's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the subscribed podcast.
    /// </summary>
    [Required]
    public int PodcastId { get; init; }

    /// <summary>
    /// Gets or sets the user who subscribed to the podcast.
    /// </summary>
    [ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; init; }
}
