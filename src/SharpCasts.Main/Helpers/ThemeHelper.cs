namespace SharpCasts.Main.Helpers;

/// <summary>
/// The class which contains helper methods for the application's theme.
/// </summary>
public class ThemeHelper
{
    /// <summary>
    /// Sets the theme to the given value.
    /// </summary>
    public static void SetTheme()
    {
        // TODO: Remove tight coupling in ThemeHelper.SetTheme
        switch (Settings.Theme)
        {
            case AppTheme.Light:
                Application.Current.UserAppTheme = AppTheme.Light;

                break;

            case AppTheme.Dark:
                Application.Current.UserAppTheme = AppTheme.Dark;

                break;

            case AppTheme.Unspecified:
                Application.Current.UserAppTheme = AppTheme.Unspecified;

                break;
        }
    }
}
