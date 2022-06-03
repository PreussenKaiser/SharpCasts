using SharedMauiLib;
using SharpCasts.Main.Models.Podcast.Fields;

namespace SharpCasts.Main.Services.Players;

/// <summary>
/// The service which plays audio from a url.
/// </summary>
public class PlayerService : IPlayerService
{
    /// <summary>
    /// The native audio service to play audio with.
    /// </summary>
    private readonly INativeAudioService audioService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerService"/> class.
    /// </summary>
    /// <param name="audioService">The native audio service to play audio with.</param>
    public PlayerService(INativeAudioService audioService)
        => this.audioService = audioService;

    /// <summary>
    /// Gets or sets the current episode.
    /// </summary>
    public Episode CurrentEpisode { get; set; }

    /// <summary>
    /// Gets or sets whether the service is playing or not.
    /// </summary>
    public bool IsPlaying { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="episode"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public async Task PlayAsync(Episode episode, double position = 0)
    {
        if (episode is null)
            return;

        this.CurrentEpisode = episode;

        await this.audioService.InitializeAsync(this.CurrentEpisode.AudioUrl);
    }
}
