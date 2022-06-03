using SharpCasts.Main.Models.Podcast.Fields;

namespace SharpCasts.Main.Services.Players;

/// <summary>
/// The interface which implements audio playing methods.
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="episode"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public Task PlayAsync(Episode episode, double position = 0);
}
