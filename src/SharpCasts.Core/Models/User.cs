using System.ComponentModel.DataAnnotations;

namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a user.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or initializes the user's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the user's name.
    /// </summary>
    [Required]
    public string Name { get; init; }

    /// <summary>
    /// Gets or initializes the user's password.
    /// </summary>
    [Required]
    public string Password { get; init; }
}
