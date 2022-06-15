using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCasts.Main.Helpers;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for the <see cref="SubscriptionsPage"/> content page.
/// </summary>
public partial class SubscriptionsPageViewModel : BaseViewModel
{
    /// <summary>
    /// The service to get subscribed podcasts with.
    /// </summary>
    private readonly ISubscriptionService subscriptionService;

    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Podcasts that the current user has subscribed to.
    /// </summary>
    [ObservableProperty]
    private IEnumerable<Podcast> subscribedPodcasts;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionsPageViewModel"/> class.
    /// </summary>
    /// <param name="subscriptionService">The service to get subscribed podcasts with.</param>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public SubscriptionsPageViewModel(ISubscriptionService subscriptionService,
                                      IPodcastService podcastService)
    {
        this.subscriptionService = subscriptionService;
        this.podcastService = podcastService;

        this.SubscribedPodcasts = new List<Podcast>();
    }

    /// <summary>
    /// Gets or sets the selected subscription.
    /// Null if a subscription hasn't been selected yet.
    /// </summary>
    public Podcast SelectedSubscription { get; set; }

    /// <summary>
    /// Refreshes the home page.
    /// </summary>
    [ICommand]
    private async void RefreshAsync()
    {
        this.IsBusy = true;

        this.SubscribedPodcasts = await this.GetPodcastsFromSubscriptionsAsync();

        this.IsBusy = false;
    }

    /// <summary>
    /// Navigates to the selected podcast's page.
    /// </summary>
    [ICommand]
    private async void SelectSubscriptionAsync()
    {
        if (this.SelectedSubscription is null)
            return;

        Dictionary<string, object> parameters = new()
        {
            { "Podcast", this.SelectedSubscription }
        };

        await Shell.Current.GoToAsync(
            $"{nameof(PodcastPage)}", true, parameters);
    }

    /// <summary>
    /// Fetches podcasts from the user's subscriptions.
    /// </summary>
    /// <returns>A list of podcasts that the user subscribed to.</returns>
    private async Task<List<Podcast>> GetPodcastsFromSubscriptionsAsync()
    {
        if (Settings.CurrentUser is null)
            return new List<Podcast>();

        int userId = Settings.CurrentUser.Id;
        List<Podcast> subscribedPodcasts = new();
        List<Subscription> subscriptions = await this.subscriptionService.GetSubscriptionsForAsync(userId);

        subscriptions.ForEach(async s =>
        {
            if (s.UserId == userId)
            {
                Podcast podcast = await this.podcastService.GetPodcastAsync(s.PodcastId);

                subscribedPodcasts.Add(podcast);
            }
        });

        return subscribedPodcasts;
    }
}
