using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the discover page.
/// </summary>
public partial class DiscoverPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscoverPage"/> class.
    /// </summary>
    /// <param name="viewmodel">The viewmodel for the discover page.</param>
    public DiscoverPage(DiscoverPageViewmodel viewmodel)
    {
		this.InitializeComponent();

		this.BindingContext = viewmodel;
    }
}