namespace SharpCasts.Main.Helpers;

/// <summary>
/// The class which contains helper methods for the application's theme.
/// </summary>
public class ThemeHelper
{
    /// <summary>
    /// Sets the theme to the given value.
    /// </summary>
    /// <param name="theme">The theme to set.</param>
    /// <remarks>
    /// Default value is <c>AppTheme.Light</c>
    /// </remarks>
    public static void SetTheme(AppTheme theme = AppTheme.Light)
    {
        switch (theme)
        {
            case AppTheme.Light:
                Application.Current.UserAppTheme = AppTheme.Light;

                break;

            case AppTheme.Dark:
                Application.Current.UserAppTheme = AppTheme.Dark;

                break;
        }
    }
}
