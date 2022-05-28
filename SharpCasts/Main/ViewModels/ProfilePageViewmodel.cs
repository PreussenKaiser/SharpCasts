using SharpCasts.Main.Views;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="ProfilePage"/> content page.
/// </summary>
public class ProfilePageViewmodel : BaseViewModel
{
    public ProfilePageViewmodel()
    {
        this.Title = "Profile";
    }
}
