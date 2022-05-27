using SharpCasts.Main.Models;
using SharpCasts.Main.Services;
using SharpCasts.Main.Views;
using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="MainPage">MainPage</see> content page.
/// </summary>
public class MainPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get user information with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// The service to get subscribed podcasts with.
    /// </summary>
    private readonly ISubscribedService subscribedService;

    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPageViewmodel">MainPageViewmodel</see> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public MainPageViewmodel(IUserService userService,
                             ISubscribedService subscribedService,
                             IPodcastService podcastService)
    {
        this.userService = userService;
        this.subscribedService = subscribedService;
        this.podcastService = podcastService;

        this.Title = "Home";
        this.RefreshCommand = new Command(this.OnRefresh);
    }

    /// <summary>
    /// Gets the command to execute when the page refreshes.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Gets or sets the list of podcasts.
    /// </summary>
    public IEnumerable<Podcast> Podcasts { get; set; }

    /// <summary>
    /// Refreshes the home page.
    /// </summary>
    private async void OnRefresh()
    {
        this.IsBusy = true;

        this.Podcasts = await this.podcastService.GetAllPodcasts();

        this.IsBusy = false;
    }
}
