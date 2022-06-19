using SharpCasts.Core.Models;

namespace SharpCasts.Core.Services;

/// <summary>
/// The interface which implements audio playing methods.
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Gets or sets the current playing episode.
    /// </summary>
    public Episode CurrentEpisode { get; set; }

    /// <summary>
    /// Gets or sets whether media is playing or not.
    /// </summary>
    public bool IsPlaying { get; set; }

    /// <summary>
    /// Plays a podcast episode asynchronously.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PlayAsync(Episode episode);

    /// <summary>
    /// Pauses a podcast episode asynchronously.
    /// </summary>
    /// <param name="episode">The episode to pause.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PauseAsync(Episode episode);
}
