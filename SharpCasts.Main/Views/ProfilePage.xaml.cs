using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class that represents the user's profile page.
/// </summary>
public partial class ProfilePage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfilePage"/> class.
    /// </summary>
    /// <param name="viewModel">The pages view model.</param>
    public ProfilePage(ProfilePageViewModel viewModel)
	{
		this.InitializeComponent();

		this.BindingContext = viewModel;
	}
}