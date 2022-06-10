using SharpCasts.Main.Configuration;
using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for the <see cref="MainPage"/> content page.
/// </summary>
public partial class MainPageViewModel : BaseViewModel
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
    /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
    /// </summary>
    /// <param name="subscriptionService">The service to get subscribed podcasts with.</param>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public MainPageViewModel(ISubscriptionService subscriptionService,
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

        Dictionary<string, object> args = new()
        {
            { "Podcast", this.SelectedSubscription }
        };

        await Shell.Current.GoToAsync(
            $"{nameof(PodcastPage)}", true, args);
    }

    /// <summary>
    /// Fetches podcasts from the user's subscriptions.
    /// </summary>
    /// <returns>A list of podcasts that the user subscribed to.</returns>
    private async Task<List<Podcast>> GetPodcastsFromSubscriptionsAsync()
    {
        if (Session.CurrentUser is null)
            return new List<Podcast>();

        int userId = Session.CurrentUser.Id;
        List<Podcast> subscribedPodcasts = new();
        List<Subscription> subscriptions = await this.subscriptionService.GetSubscriptionsForAsync(userId);

        subscriptions.ForEach(async s =>
        {
            if (s.UserId == userId)
            {
                Podcast podcast = await this.podcastService.GetPodcast(s.PodcastId);

                subscribedPodcasts.Add(podcast);
            }
        });

        return subscribedPodcasts;
    }
}
