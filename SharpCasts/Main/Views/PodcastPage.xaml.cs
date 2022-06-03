using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the podcast page.
/// This page displays a podcast.
/// </summary>
public partial class PodcastPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPage"/> class.
    /// </summary>
    /// <param name="viewmodel">The pages viewmodel.</param>
    public PodcastPage(PodcastPageViewmodel viewmodel)
	{
		this.InitializeComponent();

		this.BindingContext = viewmodel;
	}
}