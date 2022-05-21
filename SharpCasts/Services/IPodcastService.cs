using SharpCasts.Models;

namespace SharpCasts.Services;

/// <summary>
/// The interface which implements podcast query methods.
/// </summary>
public interface IPodcastService
{
    public Task<IEnumerable<Podcast>> GetPodcasts();
}
