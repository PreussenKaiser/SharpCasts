using CommunityToolkit.Mvvm.ComponentModel;
using SharpCasts.Main.Models.Podcast;
using SharpCasts.Main.Models.Podcast.Fields;
using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Services.Subscriptions;
using SharpCasts.Main.Views;
using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="PodcastPage"/> content page.
/// </summary>
[QueryProperty(nameof(Podcast), "Podcast")]
public partial class PodcastPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get podcast episodes with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// The service to handle podcast subscriptions.
    /// </summary>
    private readonly ISubscriptionService subscriptionService;

    /// <summary>
    /// The podcast to display.
    /// </summary>
    [ObservableProperty]
    private Podcast podcast;

    /// <summary>
    /// The list of episodes from the podcast.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(EpisodeCount))]
    private IEnumerable<Episode> episodes;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcast episodes with.</param>
    /// <param name="subscriptionService">The service to handle podcast subscriptions.</param>
    public PodcastPageViewmodel(IPodcastService podcastService,
                                ISubscriptionService subscriptionService)
    {
        this.podcastService = podcastService;
        this.subscriptionService = subscriptionService;

        // default value
        this.Podcast = new Podcast()
        {
            Id = 0,
            Title = string.Empty,
            Description = string.Empty,
            ImageUrl = string.Empty
        };

        this.SubscribeCommand = new Command(this.Subscribe);
        this.WebsiteCommand = new Command(this.Website);
    }

    /// <summary>
    /// Gets the command to execute when the user subscribes to a podcast.
    /// </summary>
    public ICommand SubscribeCommand { get; }

    /// <summary>
    /// Gets the command to execute when the link to the podcast's website it clicked.
    /// </summary>
    public ICommand WebsiteCommand { get; set; }

    /// <summary>
    /// Gets the amount of episodes from the podcast.
    /// </summary>
    public int EpisodeCount
        => this.Episodes.Count();

    /// <summary>
    /// Subscribes a user to a podcast.
    /// Called when the user opts to subscribe to a podcast.
    /// </summary>
    private void Subscribe()
    {
    }

    /// <summary>
    /// Goes to the podcast's website.
    /// Called when a link to the podcast's website is clicked.
    /// </summary>
    private void Website()
    {
    }
}
