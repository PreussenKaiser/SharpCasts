using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the main page.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the main page.</param>
    public MainPage(MainPageViewModel viewModel)
    {
		this.InitializeComponent();

		this.BindingContext = viewModel;
    }
}

