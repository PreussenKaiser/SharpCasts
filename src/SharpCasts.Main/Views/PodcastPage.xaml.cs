using SharpCasts.Main.ViewModels;

using SharpCasts.Core.Models;

namespace SharpCasts.Main.Views;

/// <summary>
/// The code behind for the PodcastPage view.
/// </summary>
/// <remarks>
/// The <see cref="PodcastPageViewModel">viewmodel</see> for this page takes <see cref="Podcast"/> as a query parameter.
/// </remarks>
public partial class PodcastPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPage"/> class.
    /// </summary>
    /// <param name="viewModel">The pages viewmodel.</param>
    public PodcastPage(PodcastPageViewModel viewModel)
	{
		this.InitializeComponent();

		this.BindingContext = viewModel;
	}
}