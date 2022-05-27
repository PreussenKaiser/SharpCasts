using SharpCasts.Main.Views;
using SharpCasts.Main.Services.Podcasts;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="DiscoverPage">DiscoverPage</see> content page.
/// </summary>
public class DiscoverPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscoverPageViewmodel">DiscoverPageViewmodel</see> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public DiscoverPageViewmodel(IPodcastService podcastService)
    {
        this.podcastService = podcastService;

        this.Title = "Discover";
    }
}
