using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class that represents the login page.
/// </summary>
public partial class LoginPage : ContentPage
{
	/// <summary>
	/// Initializes a new instance of the <see cref="LoginPage"/> content page.
	/// </summary>
	/// <param name="viewmodel">The viewmodel for the page.</param>
	public LoginPage(LoginPageViewmodel viewmodel)
	{
		this.InitializeComponent();

		this.BindingContext = viewmodel;
	}
}