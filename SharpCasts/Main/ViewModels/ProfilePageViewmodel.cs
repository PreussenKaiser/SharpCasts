using SharpCasts.Main.Views;
using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="ProfilePage"/> content page.
/// </summary>
public partial class ProfilePageViewmodel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfilePageViewmodel"/> viewmodel.
    /// </summary>
    public ProfilePageViewmodel()
    {
        this.AppearingCommand = new Command(this.AppearingAsync);
        this.SettingsCommand = new Command(this.SettingsAsync);
    }

    /// <summary>
    /// Gets the command to execute when the page appears.
    /// </summary>
    public ICommand AppearingCommand { get; }

    /// <summary>
    /// Gets the command to execute when the user opts to navigate to the settings page.
    /// </summary>
    public ICommand SettingsCommand { get; }

    /// <summary>
    /// Navigates to a page depending on the user's status.
    /// Called when the page appears.
    /// </summary>
    private async void AppearingAsync()
    {
        string destination = App.CurrentUser is null
            ? nameof(LoginPage)
            : nameof(ProfilePage);

        await Shell.Current.GoToAsync(destination);
    }

    /// <summary>
    /// Navigates to the settings page.
    /// Called when the user opts to navigate to the settings page.
    /// </summary>
    private async void SettingsAsync()
        => await Shell.Current.GoToAsync(nameof(SettingsPage));
}
