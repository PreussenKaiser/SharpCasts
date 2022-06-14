using SharpCasts.Main.Views;
using SharpCasts.Main.Helpers;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for the <see cref="SettingsPage"/> content page.
/// </summary>
public partial class SettingsPageViewModel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPageViewModel"/> class.
    /// </summary>
    public SettingsPageViewModel()
    {
    }

    /// <summary>
    /// Gets or sets whether the application's dark mode is enabled or not.
    /// </summary>
    public bool DarkModeEnabled
    {
        get => Settings.Theme == AppTheme.Dark;
        set => Settings.Theme = value
                ? AppTheme.Dark
                : AppTheme.Light;
    }

    /// <summary>
    /// Gets the current version of the application.
    /// </summary>
    public string AppVersion
    {
        get
        {
            string version = "SharpCasts ";

#if DEBUG
            version += "DEBUG";
#else
            version += $"v{VersionTracking.CurrentVersion}";
#endif

            return version;
        }
    }
}
