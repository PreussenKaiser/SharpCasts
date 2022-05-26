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
    public Task<IEnumerable<Podcast>> GetPodcasts();
}
