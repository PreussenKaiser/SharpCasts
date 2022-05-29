using SharpCasts.Main.Views;
using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Models;

using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="DiscoverPage"/> content page.
/// </summary>
public class DiscoverPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// A list of podcasts that match the search.
    /// </summary>
    private IEnumerable<Podcast> podcasts;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscoverPageViewmodel">DiscoverPageViewmodel</see> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public DiscoverPageViewmodel(IPodcastService podcastService)
    {
        this.podcastService = podcastService;
        this.podcasts = new ObservableCollection<Podcast>();

        this.Title = "Discover";
        this.RefreshCommand = new Command<string>(this.Search);
        this.SearchCommand = new Command<string>(this.Search);
        this.SelectPodcastCommand = new Command<Podcast>(this.PodcastSelected);
    }

    /// <summary>
    /// Gets the command to execute when the page is manually refreshed.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Gets the command to execute when a value is searched.
    /// </summary>
    public ICommand SearchCommand { get; }

    /// <summary>
    /// Gets the command to execute when a podcast is selected.
    /// </summary>
    public ICommand SelectPodcastCommand { get; }

    /// <summary>
    /// Gets or sets a list of podcasts that match the search.
    /// </summary>
    public IEnumerable<Podcast> Podcasts
    {
        get => this.podcasts;
        set
        {
            this.podcasts = value;
            this.OnPropertyChanged(nameof(this.Podcasts));
        }
    }

    /// <summary>
    /// Searches for podcasts.
    /// </summary>
    /// <param name="search">The search term to use.</param>
    private async void Search(string search)
    {
        this.IsBusy = true;

        this.Podcasts = await this.podcastService.SearchPodcasts(search);

        this.IsBusy = false;
    }

    /// <summary>
    /// Navigates to the selected podcast's page.
    /// </summary>
    /// <param name="podcast">The podcast to navigate to.</param>
    private async void PodcastSelected(Podcast podcast)
    {

    }
}
