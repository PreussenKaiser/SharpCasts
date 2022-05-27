using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services;

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
}
