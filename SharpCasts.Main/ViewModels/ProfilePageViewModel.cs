using SharpCasts.Main.Configuration;
using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using MenuItem = SharpCasts.Core.Models.MenuItem;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

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
                Name = "Subscriptions",
                Route = string.Empty
            },
            new()
            {
                Icon = string.Empty,
                Name = "Downloads",
                Route = string.Empty
            },
            new()
            {
                Icon = string.Empty,
                Name = "Settings",
                Route = nameof(SettingsPage)
            }
        };

        this.AppearingCommand = new Command(this.Appearing);
        this.LoginCommand = new Command(this.LoginAsync);
        this.RegisterCommand = new Command(this.RegisterAsync);
        this.MenuItemSelectedCommand = new Command(this.MenuItemSelectedAsync);
    }

    /// <summary>
    /// Gets the command to execute when the page appears.
    /// </summary>
    public ICommand AppearingCommand { get; }

    /// <summary>
    /// Gets the command to execute when the user wants to log in.
    /// </summary>
    public ICommand LoginCommand { get; }

    /// <summary>
    /// Gets the command to execute when the user wants to register.
    /// </summary>
    public ICommand RegisterCommand { get; }

    /// <summary>
    /// Gets the command to execute when a menu item is selected.
    /// </summary>
    public ICommand MenuItemSelectedCommand { get; }

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
    /// <remarks>
    /// Called when <see cref="AppearingCommand"/> is executed.
    /// </remarks>
    private void Appearing()
        => this.CurrentUser ??= Session.CurrentUser;

    /// <summary>
    /// Sends the user to the login page.
    /// Called when <see cref="LoginCommand"/> is executed.
    /// </summary>
    private async void LoginAsync()
        => await Shell.Current.GoToAsync(nameof(LoginPage));

    /// <summary>
    /// Sends the user to the register page.
    /// Called when <see cref="RegisterCommand"/> is executed.
    /// </summary>
    private async void RegisterAsync()
        => await Shell.Current.GoToAsync(nameof(RegisterPage));

    /// <summary>
    /// Executes a menu item's action.
    /// </summary>
    /// <remarks>
    /// Called when <see cref="MenuItemSelectedCommand"/> is executed.
    /// </remarks>
    private async void MenuItemSelectedAsync()
    {
        if (this.SelectedMenuItem is null)
            return;

        await Shell.Current.GoToAsync(this.SelectedMenuItem.Route);
    }
}
