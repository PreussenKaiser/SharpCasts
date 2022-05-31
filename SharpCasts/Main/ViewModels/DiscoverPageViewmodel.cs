using SharpCasts.Main.Views;
using SharpCasts.Main.Services.Podcasts;
using System.Windows.Input;
using SharpCasts.Main.Models.Podcast;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="DiscoverPage"/> content page.
/// </summary>
public partial class DiscoverPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// A list of podcasts that match the search.
    /// </summary>
    [ObservableProperty]
    private IEnumerable<Podcast> podcasts;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscoverPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public DiscoverPageViewmodel(IPodcastService podcastService)
    {
        this.podcastService = podcastService;

        this.Title = "Discover";
        this.Podcasts = new List<Podcast>();

        this.RefreshCommand = new Command<string>(this.Search);
        this.SearchCommand = new Command<string>(this.Search);
        this.SelectPodcastCommand = new Command(this.PodcastSelected);
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
    /// Gets or sets the currently selected podcast.
    /// </summary>
    public Podcast SelectedPodcast { get; set; }

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
    private async void PodcastSelected()
    {
        if (this.SelectedPodcast is null)
            return;

        Dictionary<string, object> args = new()
        {
            { "Podcast", this.SelectedPodcast }
        };

        _ = await this.podcastService.GetEpisodes(this.SelectedPodcast.Id);

        await Shell.Current.GoToAsync(
            $"{nameof(PodcastPage)}", true, args);
    }
}
