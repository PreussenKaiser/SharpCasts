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
        this.Title = "Profile";

        this.AppearingCommand = new Command(this.Appearing);
    }

    /// <summary>
    /// Gets the command to execute when the page appears.
    /// </summary>
    public ICommand AppearingCommand { get; }

    /// <summary>
    /// Navigates to a page depending on the user's status.
    /// Called when the page appears.
    /// </summary>
    private async void Appearing()
    {
        string destination = App.CurrentUser is null
            ? nameof(LoginPage)
            : nameof(ProfilePage);

        await Shell.Current.GoToAsync(destination);
    }
}
