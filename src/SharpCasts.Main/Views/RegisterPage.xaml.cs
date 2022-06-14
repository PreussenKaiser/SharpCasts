using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the RegisterPage view.
/// </summary>
public partial class RegisterPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the page.</param>
    public RegisterPage(RegisterPageViewModel viewModel)
	{
		this.InitializeComponent();

		this.BindingContext = viewModel;
	}
}