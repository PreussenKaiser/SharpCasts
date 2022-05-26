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
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes the viewmodel's services.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public MainPageViewmodel(IPodcastService podcastService)
    {
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

        this.Podcasts = await this.podcastService.GetPodcasts();

        this.IsBusy = false;
    }
}
