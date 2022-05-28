using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the main page.
/// </summary>
public partial class MainPage : ContentPage
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MainPage"/> content page.
	/// </summary>
	/// <param name="viewmodel">The viewmodel for the main page.</param>
	public MainPage(MainPageViewmodel viewmodel)
    {
		this.InitializeComponent();

		this.BindingContext = viewmodel;
    }
}

