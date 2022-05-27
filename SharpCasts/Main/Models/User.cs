namespace SharpCasts.Main.Models;

/// <summary>
/// The class which represents a user.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the user's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public string Password { get; set; }
}
