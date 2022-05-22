using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services;

/// <summary>
/// The interface which implements podcast query methods.
/// </summary>
public interface IPodcastService
{
    public Task<IEnumerable<Podcast>> GetPodcasts();
}
