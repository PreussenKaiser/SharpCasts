using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the DiscoverPage view.
/// </summary>
public partial class DiscoverPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscoverPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the discover page.</param>
    public DiscoverPage(DiscoverPageViewModel viewModel)
    {
		this.InitializeComponent();

		this.BindingContext = viewModel;
    }
}