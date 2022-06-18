using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Represents the page to view a podcast episode.
/// </summary>
public partial class EpisodePage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EpisodePage"/> class.
    /// </summary>
    /// <param name="viewModel">The pages view model.</param>
	public EpisodePage(EpisodePageViewModel viewModel)
	{
		this.InitializeComponent();

        this.BindingContext = viewModel;
	}
}