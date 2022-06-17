using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for the <see cref="DiscoverPage"/> content page.
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
    }

    /// <summary>
    /// Gets or sets the currently selected podcast.
    /// </summary>
    public Podcast SelectedPodcast { get; set; }

    /// <summary>
    /// Searches for podcasts.
    /// </summary>
    /// <param name="search">The search term to use.</param>
    [ICommand]
    private async void SearchAsync(string search)
    {
        this.IsBusy = true;

        try
        {
            this.Podcasts = await this.podcastService.SearchPodcastsAsync(search);
        }
        catch (HttpRequestException)
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
    [ICommand]
    private async void SelectPodcastAsync()
    {
        if (this.SelectedPodcast is null)
            return;

        Dictionary<string, object> parameters = new()
        {
            { "Podcast", this.SelectedPodcast }
        };

        await Shell.Current.GoToAsync(
            $"{nameof(PodcastPage)}", true, parameters);
    }
}
