using SharpCasts.Main.Models.Podcast;
using SharpCasts.Main.Models.Podcast.Fields;
using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Views;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="PodcastPage"/> content page.
/// </summary>
[QueryProperty(nameof(Podcast), "Podcast")]
public class PodcastPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get podcast episodes with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// The podcast to display.
    /// </summary>
    private Podcast podcast;

    /// <summary>
    /// The list of episodes from the podcast.
    /// </summary>
    private IEnumerable<Episode> episodes;

    /// <summary>
    /// The amount of episodes from the podcast.
    /// </summary>
    private int episodeCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="podcastService">The service to get podcast episodes with.</param>
    public PodcastPageViewmodel(IPodcastService podcastService)
    {
        this.podcastService = podcastService;

        // default value
        this.podcast = new Podcast()
        {
            Id = 0,
            Title = string.Empty,
            Description = string.Empty,
            ImageUrl = string.Empty
        };
    }

    /// <summary>
    /// Gets the podcast to display.
    /// </summary>
    public Podcast Podcast
    {
        get => this.podcast;
        set
        {
            this.podcast = value;

            this.OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the list of episodes from the podcast.
    /// </summary>
    public IEnumerable<Episode> Episodes
    {
        get => this.episodes;
        set
        {
            this.episodes = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the amount of episodes from the podcast.
    /// </summary>
    public int EpisodeCount
    {
        get => this.episodeCount;
        set
        {
            this.episodeCount = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    /// Sets episode data from the podcast in the view.
    /// </summary>
    private void SetPodcastData()
    {
        this.Episodes = this.podcastService
                            .GetEpisodes(this.Podcast.Id)
                            .Result;

        this.EpisodeCount = this.Episodes.Count();
    }
}
