using SharpCasts.Main.Models.Podcast;
using SharpCasts.Main.Models.Podcast.Fields;

namespace SharpCasts.Main.Services.Podcasts;

/// <summary>
/// The interface which implements podcast query methods.
/// </summary>
public interface IPodcastService
{
    /// <summary>
    /// Gets a podcast from the service.
    /// </summary>
    /// <param name="podcastId">The identifier of the podcast to get.</param>
    /// <returns>The podcast which matched the supplied identifier.</returns>
    public Task<Podcast> GetPodcast(int podcastId);

    /// <summary>
    /// Searches the service for podcasts that match the search term.
    /// </summary>
    /// <param name="search">The search term to filter with.</param>
    /// <returns>Podcasts that match the search term.</returns>
    public Task<List<Podcast>> SearchPodcasts(string search);

    /// <summary>
    /// Gets a list of episodes from a podcast.
    /// </summary>
    /// <param name="podcastId">The unique identifier of the podcast to get episodes from.</param>
    /// <returns>A list of episodes from that podcast.</returns>
    public Task<List<Episode>> GetEpisodes(int podcastId);
}
