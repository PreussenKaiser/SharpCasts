namespace SharpCasts.Main.Helpers;

/// <summary>
/// Contains helper methods for the application's theme.
/// </summary>
public class ThemeHelper
{
    /// <summary>
    /// Sets the theme according to what's in application settings..
    /// </summary>
    public static void SetTheme()
        => Application.Current.UserAppTheme = Settings.Theme;
}
