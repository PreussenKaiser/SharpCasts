using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services.Podcasts;

/// <summary>
/// The interface which implements podcast query methods.
/// </summary>
public interface IPodcastService
{
    /// <summary>
    /// Gets podcasts from the service.
    /// </summary>
    /// <returns>Podcasts in the service.</returns>
    public Task<IEnumerable<Podcast>> GetAllPodcasts();

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
}
