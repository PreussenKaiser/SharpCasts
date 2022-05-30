using SharpCasts.Main.Views;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="ProfilePage"/> content page.
/// </summary>
public class ProfilePageViewmodel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfilePageViewmodel"/> viewmodel.
    /// </summary>
    public ProfilePageViewmodel()
    {
        this.Title = "Profile";
    }
}
