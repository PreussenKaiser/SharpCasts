using SharpCasts.Core.Models;

namespace SharpCasts.Main.Configuration;

/// <summary>
/// The class which contains application session data.
/// </summary>
public static class Session
{
    /// <summary>
    /// Gets whether the application is on desktop or not.
    /// </summary>
    public static bool IsDesktop
    {
        get
        {
#if WINDOWS
            return true;
#endif
            return false;
        }
    }

    /// <summary>
    /// Gets or sets the current logged in user.
    /// </summary>
    public static User CurrentUser { get; set; }
}
