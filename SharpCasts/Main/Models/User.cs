using System.ComponentModel.DataAnnotations;

namespace SharpCasts.Main.Models;

/// <summary>
/// The class which represents a user.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the user's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    [Required]
    public string Name { get; init; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    [Required]
    public string Password { get; init; }
}
