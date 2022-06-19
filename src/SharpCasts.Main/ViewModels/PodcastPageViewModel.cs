using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCasts.Main.Helpers;
using SharpCasts.Core.Exceptions;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for the <see cref="PodcastPage"/> content page.
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
    /// The podcast to display.
    /// </summary>
    private Podcast podcast;

    /// <summary>
    /// The episodes from the currently displayed podcast.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(EpisodeCount))]
    private IEnumerable<Episode> episodes;

    /// <summary>
    /// Whether the podcast has already been subscribed to or not.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(IsNotSubscribed))]
    private bool isSubscribed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPageViewModel"/> class.
    /// </summary>
    /// <param name="podcastService">The service to get podcast episodes with.</param>
    /// <param name="subscriptionService">The service to handle podcast subscriptions.</param>
    public PodcastPageViewModel(IPodcastService podcastService,
                                ISubscriptionService subscriptionService)
    {
        this.podcastService = podcastService;
        this.subscriptionService = subscriptionService;
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
        => $"{this.Episodes.Count()} Episodes";

    /// <summary>
    /// Gets whether the user hasn't subscribed to the podcast or not.
    /// </summary>
    public bool IsNotSubscribed
        => !this.IsSubscribed;

    /// <summary>
    /// Updates the podcast's list of episodes.
    /// </summary>
    [ICommand]
    private async void RefreshAsync()
    {
        if (this.Podcast is null)
            return;

        this.IsBusy = true;

        await Task.Run(async () =>
        {
            Channel channel = await this.podcastService.GetChannelAsync(this.Podcast.Feed);

            this.Podcast.Author = channel.Author;
            this.Podcast.Description = channel.Description;
            this.Podcast.Website = channel.Website;
            this.Episodes = channel.Episodes;
        });

        this.IsSubscribed = await this.IfSubscribed();

        this.IsBusy = false;
    }

    /// <summary>
    /// Subscribes a user to a podcast.
    /// Called when the user opts to subscribe to a podcast.
    /// </summary>
    [ICommand]
    private async void SubscribeAsync()
    {
        if (Settings.CurrentUser is null)
        {
            await Shell.Current.DisplayAlert(
                    "Could not subscribe",
                    "You need to be logged in to subscribe",
                    "OK");

            return;
        }

        Subscription subscription = new()
        {
            UserId = Settings.CurrentUser.Id,
            PodcastId = this.Podcast.Id
        };

        await this.subscriptionService.AddSubscriptionAsync(subscription);
        this.IsSubscribed = true;
    }

    /// <summary>
    /// Unsubscribes the user from the podcast.
    /// </summary>
    [ICommand]
    private async void UnsubscribeAsync()
    {
        if (Settings.CurrentUser is null)
            return;

        Subscription subscription = await this.subscriptionService.GetSubscriptionAsync(Settings.CurrentUser.Id, this.Podcast.Id);

        if (subscription is null)
            return;

        await this.subscriptionService.UnsubscribeAsync(subscription);
        this.IsSubscribed = false;
    }

    /// <summary>
    /// Goes to the podcast's website.
    /// </summary>
    [ICommand]
    private async void GoToWebsiteAsync()
        => await Browser.OpenAsync(this.Podcast.Website);

    /// <summary>
    /// Navigates to <see cref="EpisodePage"/> asynchronously.
    /// </summary>
    /// <param name="episode">The episode to pass into the page.</param>\
    [ICommand]
    private async void GoToEpisodeAsync(Episode episode)
    {
        if (episode is null)
            return;

        Dictionary<string, object> args = new()
        {
            { "Episode", episode }
        };

        await Shell.Current.GoToAsync(
            $"{nameof(EpisodePage)}", true, args);
    }

    /// <summary>
    /// Determines if the current user has subscribed to this podcast.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task<bool> IfSubscribed()
    {
        if (Settings.CurrentUser is null || this.Podcast is null)
            return false;

        Subscription subscription = await this.subscriptionService.GetSubscriptionAsync(
                                        Settings.CurrentUser.Id,
                                        this.Podcast.Id);

        return subscription is not null;
    }
}
