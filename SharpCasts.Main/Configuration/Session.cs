using SharpCasts.Core.Models;

namespace SharpCasts.Main.Configuration;

/// <summary>
/// The class which contains application session data.
/// </summary>
public static class Session
{
    /// <summary>
    /// Gets or sets the current logged in user.
    /// </summary>
    public static User CurrentUser { get; set; }
}
