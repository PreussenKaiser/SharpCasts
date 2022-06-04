using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services.Players;

/// <summary>
/// The interface which implements audio playing methods.
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Plays audio asynchronously.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    /// <param name="position">Where to start playing from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PlayAsync(Episode episode, double position = 0);
}
