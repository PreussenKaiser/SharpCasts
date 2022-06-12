using SharpCasts.Main.Configuration;
using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for the <see cref="PodcastPage"/> content page.
/// </summary>
[QueryProperty(nameof(Podcast), "Podcast")]
public partial class PodcastPageViewModel : BaseViewModel
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
    /// The service to play audio with.
    /// </summary>
    private readonly IPlayerService playerService;

    /// <summary>
    /// The podcast to display.
    /// </summary>
    private Podcast podcast;

    /// <summary>
    /// The episodes from the currently displayed podcast.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(EpisodeCount))]
    private List<Episode> episodes;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPageViewModel"/> class.
    /// </summary>
    /// <param name="podcastService">The service to get podcast episodes with.</param>
    /// <param name="subscriptionService">The service to handle podcast subscriptions.</param>
    public PodcastPageViewModel(IPodcastService podcastService,
                                ISubscriptionService subscriptionService,
                                IPlayerService playerService)
    {
        this.podcastService = podcastService;
        this.subscriptionService = subscriptionService;
        this.playerService = playerService;
    }

    /// <summary>
    /// Gets or sets the podcast to display.
    /// </summary>
    public Podcast Podcast
    {
        get => this.podcast;
        set
        {
            this.SetProperty(ref this.podcast, value);
            this.RefreshAsync();
        }
    }

    /// <summary>
    /// Gets the podcast's amount of episodes.
    /// </summary>
    public string EpisodeCount
        => $"{this.Episodes.Count} Episodes";

    /// <summary>
    /// Updates the podcast's list of episodes.
    /// </summary>
    [ICommand]
    private async void RefreshAsync()
    {
        if (this.Podcast is null)
            return;

        this.IsBusy = true;

        this.Episodes = await this.podcastService
                                  .GetEpisodes(this.Podcast.Id);

        this.IsBusy = false;
    }

    /// <summary>
    /// Subscribes a user to a podcast.
    /// Called when the user opts to subscribe to a podcast.
    /// </summary>
    [ICommand]
    private async void SubscribeAsync()
    {
        if (Session.CurrentUser is null)
        {
            await Shell.Current.DisplayPromptAsync(
                "Could not subscribe",
                "You need to be logged in to subscribe",
                "OK");

            return;
        }

        Subscription subscription = new()
        {
            UserId = Session.CurrentUser.Id,
            PodcastId = this.Podcast.Id
        };

        await this.subscriptionService.AddSubscriptionAsync(subscription);
    }

    /// <summary>
    /// Plays the currently selected episode.
    /// Called when the user opts to play an episode.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    [ICommand]
    private async void PlayAsync(Episode episode)
        => await this.playerService.PlayAsync(episode, this.Podcast);
}
