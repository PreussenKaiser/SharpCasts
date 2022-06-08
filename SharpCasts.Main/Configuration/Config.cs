namespace SharpCasts.Main.Configuration;

/// <summary>
/// The class which runs configuration depending on the application's state.
/// </summary>
public static class Config
{
    /// <summary>
    /// Gets whether the application is on desktop or not.
    /// </summary>
    public static bool Desktop
    {
        get
        {
#if WINDOWS
            return true;
#endif
            return false;
        }
    }
}
