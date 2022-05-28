using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class that represents the user's profile page.
/// </summary>
public partial class ProfilePage : ContentPage
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ProfilePage"/> content page.
	/// </summary>
	/// <param name="viewmodel">The pages viewmodel.</param>
	public ProfilePage(ProfilePageViewmodel viewmodel)
	{
		this.InitializeComponent();

		this.BindingContext = viewmodel;
	}
}