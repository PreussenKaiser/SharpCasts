using SharpCasts.Core.Models;

namespace SharpCasts.Main.Helpers;

/// <summary>
/// Contains application settings.
/// </summary>
public static class Settings
{
    /// <summary>
    /// The application's default theme.
    /// </summary>
    private const AppTheme DEFAULT_THEME = AppTheme.Unspecified;

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

    /// <summary>
    /// Gets or sets the application's theme.
    /// </summary>
    public static AppTheme Theme
    {
        get => Enum.Parse<AppTheme>(Preferences.Get(nameof(Theme), Enum.GetName(DEFAULT_THEME)));
        set
        {
            Preferences.Set(nameof(Theme), value.ToString());
            ThemeHelper.SetTheme();
        }
    }

    /// <summary>
    /// Gets or sets whether to store user data locally or not.
    /// </summary>
    public static bool UseLocal
    {
        get => Preferences.Get(nameof(UseLocal), true);
        set => Preferences.Set(nameof(UseLocal), value);
    }
}
