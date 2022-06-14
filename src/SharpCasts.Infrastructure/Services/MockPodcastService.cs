using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// Represents an imitation of a podcast service.
/// </summary>
public class MockPodcastService : IPodcastService
{
    /// <summary>
    /// The enumerable of podcasts in the service.
    /// </summary>
    private readonly IEnumerable<Podcast> podcasts;

    /// <summary>
    /// Initializes a new instance of the<see cref="MockPodcastService"/> class.
    /// </summary>
    public MockPodcastService()
        => this.podcasts = new List<Podcast>
        {
            new()
            {
                Id = 1,
                Title = "Command Line Heroes",
                Description = "Command Line Heroes tells the epic true tales of how developers, programmers, hackers, geeks, and open source rebels are revolutionizing the technology landscape.",
                Author = new EmailContact { Name = "Red Hat" },
                ImageUrl = "https://pacific-content.com/wp-content/uploads/2020/09/Command-Line-Heroes-tile-artwork.png",
                Episodes = new EpisodeList { List = new List<Episode>
                    {
                        new()
                        {
                            Title = "Relentless Replicators",
                            Description = "Computer viruses and worms haunt the internet. They worm their way into a system, replicate, and spread again. It’s a simple process—with devastating consequences. But there’s a whole industry of people that rose up to fight back. Craig Schmugar recalls how he and his team responded to MyDoom, one of the fastest-spreading worms ever. Dr. Nur Zincir-Heywood reveals the inner workings of viruses and worms, and how they draw their names from the world of biology. And security expert Mikko Hypponen shares advice on avoiding malware. But he also warns that we’re in an arms race against malware developers.",
                            Date = "2022-06-02 00:00:00",
                            AudioUrl = "https://cdn.simplecast.com/audio/a88fbe81-5614-4834-8a78-24c287debbe6/episodes/a802eb52-ef76-4514-8283-9657e740b0a4/audio/90e8ef59-d481-4d02-9de7-6379490d3e1d/default_tc.mp3"
                        }
                    }
                }
            }
        };

    /// <summary>
    /// Gets episodes from a podcast in the mock data store.
    /// </summary>
    /// <param name="podcastId">The identifier of the podcast to get episodes from.</param>
    /// <returns>A list of episodes from that podcast.</returns>
    public async Task<List<Episode>> GetEpisodesAsync(int podcastId)
    {
        Podcast podcast = this.podcasts.First(p => p.Id == podcastId);
        List<Episode> episodes = await Task.FromResult(podcast.Episodes.List);

        return episodes;
    }

    /// <summary>
    /// Gets a podcast from the mock data store.
    /// </summary>
    /// <param name="podcastId"></param>
    /// <returns></returns>
    public async Task<Podcast> GetPodcastAsync(int podcastId)
    {
        Podcast podcast = this.podcasts.First(p => p.Id == podcastId);
        podcast = await Task.FromResult(podcast);

        return podcast;
    }

    /// <summary>
    /// Gets podcasts from the mock data store that match the search term.
    /// </summary>
    /// <param name="search">The podcasts to search for.</param>
    /// <returns>Podcasts that match the search term.</returns>
    public async Task<List<Podcast>> SearchPodcastsAsync(string search)
    {
        List<Podcast> podcasts = await Task.FromResult(this.podcasts.ToList());

        return podcasts;
    }
}
