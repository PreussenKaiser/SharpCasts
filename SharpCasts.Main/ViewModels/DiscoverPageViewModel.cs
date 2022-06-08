using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for the <see cref="DiscoverPage"/> content page.
/// </summary>
public partial class DiscoverPageViewModel : BaseViewModel
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
    /// Initializes a new instance of the <see cref="DiscoverPageViewModel"/> class.
    /// </summary>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public DiscoverPageViewModel(IPodcastService podcastService)
    {
        this.podcastService = podcastService;

        this.Podcasts = new List<Podcast>();

        this.RefreshCommand = new Command<string>(this.SearchAsync);
        this.SearchCommand = new Command<string>(this.SearchAsync);
        this.SelectPodcastCommand = new Command(this.PodcastSelectedAsync);
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
    private async void SearchAsync(string search)
    {
        this.IsBusy = true;

        try
        {
            this.Podcasts = await this.podcastService.SearchPodcasts(search);
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error",
                                             "There was a problem gettings podcasts",
                                             "OK");
        }

        this.IsBusy = false;
    }

    /// <summary>
    /// Navigates to the selected podcast's page.
    /// </summary>
    private async void PodcastSelectedAsync()
    {
        if (this.SelectedPodcast is null)
            return;

        Dictionary<string, object> args = new()
        {
            { "Podcast", this.SelectedPodcast }
        };

        await Shell.Current.GoToAsync(
            $"{nameof(PodcastPage)}", true, args);
    }
}
