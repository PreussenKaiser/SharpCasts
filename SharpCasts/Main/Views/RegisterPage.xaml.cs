using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the register page.
/// </summary>
public partial class RegisterPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterPage"/> class.
    /// </summary>
    /// <param name="viewmodel">The viewmodel for the page.</param>
    public RegisterPage(RegisterPageViewmodel viewmodel)
	{
		this.InitializeComponent();

		this.BindingContext = viewmodel;
	}
}