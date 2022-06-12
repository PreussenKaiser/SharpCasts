using SharpCasts.Main.Configuration;
using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using MenuItem = SharpCasts.Core.Models.MenuItem;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for the <see cref="ProfilePage"/> content page.
/// </summary>
public partial class ProfilePageViewModel : BaseViewModel
{
    /// <summary>
    /// The current user to display.
    /// </summary>
    /// <remarks>
    /// Will return null if a user is not logged in.
    /// </remarks>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(IsLoggedIn))]
    [AlsoNotifyChangeFor(nameof(IsNotLoggedIn))]
    private User currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfilePageViewModel"/> class.
    /// </summary>
    public ProfilePageViewModel()
    {
        this.ProfileMenuItems = new List<MenuItem>
        {
            new()
            {
                Icon = string.Empty,
                Name = "Downloads",
                Route = string.Empty
            },
            new()
            {
                Icon = "settings.png",
                Name = "Settings",
                Route = nameof(SettingsPage)
            }
        };
    }

    /// <summary>
    /// Gets the menu items for the profile navigation menu.
    /// </summary>
    public IEnumerable<MenuItem> ProfileMenuItems { get; }

    /// <summary>
    /// Gets or sets the selected menu item.
    /// </summary>
    public MenuItem SelectedMenuItem { get; set; }

    /// <summary>
    /// Gets whether a user is logged in or not.
    /// </summary>
    public bool IsLoggedIn
        => this.CurrentUser is not null;

    /// <summary>
    /// Gets whether the user is not logged in or not.
    /// </summary>
    public bool IsNotLoggedIn
        => this.CurrentUser is null;

    /// <summary>
    /// Checks if a user has signed in.
    /// </summary>
    [ICommand]
    private void Appearing()
        => this.CurrentUser ??= Session.CurrentUser;

    /// <summary>
    /// Sends the user to the login page.
    /// </summary>
    [ICommand]
    private async void LoginAsync()
        => await Shell.Current.GoToAsync(nameof(LoginPage));

    /// <summary>
    /// Sends the user to the register page.
    /// </summary>
    [ICommand]
    private async void RegisterAsync()
        => await Shell.Current.GoToAsync(nameof(RegisterPage));

    /// <summary>
    /// Logs the user out of the current session.
    /// </summary>
    [ICommand]
    private void Logout()
    {
        Session.CurrentUser = null;
        this.CurrentUser = null;
    }

    /// <summary>
    /// Executes a menu item's action.
    /// </summary>
    [ICommand]
    private async void SelectMenuItemAsync()
    {
        if (this.SelectedMenuItem is null)
            return;

        await Shell.Current.GoToAsync(this.SelectedMenuItem.Route);
    }
}
