using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the podcast page.
/// This page displays a podcasts.
/// </summary>
[QueryProperty(nameof(PodcastId), "podcast")]
public partial class PodcastPage : ContentPage
{
    /// <summary>
    /// The viewmodel for the page.
    /// </summary>
    private readonly PodcastPageViewmodel viewmodel;

    /// <summary>
    /// The service to get a podcast matching the suppied identifier.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPage"/> content page.
    /// </summary>
    public PodcastPage(PodcastPageViewmodel viewmodel,
                       IPodcastService podcastService)
	{
		this.InitializeComponent();

        this.viewmodel = viewmodel;
        this.podcastService = podcastService;

		this.BindingContext = viewmodel;
	}

    /// <summary>
    /// Gets or sets the unique identifier of the podcast to display.
    /// </summary>
    public int PodcastId { get; set; }
}