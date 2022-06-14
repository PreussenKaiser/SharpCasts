using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the LoginPage view.
/// </summary>
public partial class LoginPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the page.</param>
    public LoginPage(LoginPageViewModel viewModel)
	{
		this.InitializeComponent();

		this.BindingContext = viewModel;
	}
}