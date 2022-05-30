using SharpCasts.Main.Models.Podcast;
using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Services.Subscriptions;
using SharpCasts.Main.Views;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="MainPage"/> content page.
/// </summary>
public class MainPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get subscribed podcasts with.
    /// </summary>
    private readonly ISubscribedService subscribedService;

    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="subscribedService">The service to get subscribed podcasts with.</param>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public MainPageViewmodel(ISubscribedService subscribedService,
                             IPodcastService podcastService)
    {
        this.subscribedService = subscribedService;
        this.podcastService = podcastService;

        this.Title = "Home";
        this.RefreshCommand = new Command(this.OnRefresh);
        this.ViewProfileCommand = new Command(this.ViewProfile);
    }

    /// <summary>
    /// Gets the command to execute when the page refreshes.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Gets the command to execute when the user navigates to their profile.
    /// </summary>
    public ICommand ViewProfileCommand { get; }

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
        this.IsBusy = false;
    }

    /// <summary>
    /// Navigates to a page depending on the user's status.
    /// </summary>
    private async void ViewProfile()
    {
        string destination = App.CurrentUser is null
            ? nameof(LoginPage)
            : nameof(ProfilePage);

        await Shell.Current.GoToAsync(destination);
    }
}
