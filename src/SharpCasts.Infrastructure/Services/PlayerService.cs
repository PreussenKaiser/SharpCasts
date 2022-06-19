using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using SharpCasts.Audio;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// Plays audio from a url using native APIs.
/// </summary>
public class PlayerService : IPlayerService
{
    /// <summary>
    /// Executes when the current track changes.
    /// </summary>
    public event EventHandler IsPlayingChanged;

    /// <summary>
    /// Executes when an episodes is added to the player.
    /// </summary>
    public event EventHandler NewEpisodeAdded;

    /// <summary>
    /// The native audio service to play audio with.
    /// </summary>
    private readonly INativeAudioService audioService;

    /// <summary>
    /// Whether audio is playing or not.
    /// </summary>
    private bool isPlaying;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerService"/> class.
    /// </summary>
    /// <param name="audioService">The native audio service to play audio with.</param>
    public PlayerService(INativeAudioService audioService)
    {
        this.audioService = audioService;

        this.audioService.IsPlayingChanged += (object sender, bool e) =>
        {
            this.IsPlaying = e;
            this.IsPlayingChanged?.Invoke(this, EventArgs.Empty);
        };
    }

    /// <summary>
    /// Gets or sets whether the service is playing or not.
    /// </summary>
    public bool IsPlaying { get; set; }

    /// <summary>
    /// Gets or sets the current episode.
    /// </summary>
    public Episode CurrentEpisode { get; set; }

    /// <summary>
    /// Gets the player's current position.
    /// </summary>
    public double CurrentPosition
        => this.audioService.CurrentPosition;

    /// <summary>
    /// Plays a podcast episode asynchronously.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PlayAsync(Episode episode)
    {
        bool isOtherEpisode = this.CurrentEpisode?.Id != episode.Id;
        bool isPlaying = isOtherEpisode || !this.audioService.IsPlaying;
        double position = isOtherEpisode
            ? 0
            : this.CurrentPosition;

        if (this.CurrentEpisode is not null)
        {
            string message = isPlaying
                ? $"Episode with title '{this.CurrentEpisode.Title}' will start playing"
                : $"Episode with title '{this.CurrentEpisode.Title}' will be paused";

            SemanticScreenReader.Announce(message);
        }

        return this.PlayAsync(episode, isPlaying, position);
    }

    /// <summary>
    /// Pauses the currently playing episode.
    /// </summary>
    /// <param name="episode">The episode to pause</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task PauseAsync(Episode episode)
    {
        if (episode.Id != this.CurrentEpisode?.Id)
            return;

        await this.InternalPauseAsync();
    }

    /// <summary>
    /// Plays a podcast episode asynchronously.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    /// <param name="isPlaying">Whether the episode is playing or not.</param>
    /// <param name="position">Where to start playing from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task PlayAsync(Episode episode, bool isPlaying,
                                double position = 0)
    {
        if (episode is null)
            return;

        bool isOtherEpisode = this.CurrentEpisode?.Id != episode.Id;

        if (isOtherEpisode)
        {
            this.CurrentEpisode = episode;

            if (this.audioService.IsPlaying)
            {
                await this.InternalPauseAsync();
            }

            await this.audioService.InitializeAsync(this.CurrentEpisode.Audio.Url);
            await this.InternalPlayPauseAsync(isPlaying, position);

            this.NewEpisodeAdded?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            await this.InternalPlayPauseAsync(isPlaying, position);
        }

        this.IsPlayingChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Plays or pauses the track depending on if it's playing.
    /// </summary>
    /// <param name="isPlaying">Whether the track is playing or not.</param>
    /// <param name="position">Where to play the track from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task InternalPlayPauseAsync(bool isPlaying, double position)
    {
        if (isPlaying)
        {
            await this.InternalPlayAsync(position);

            return;
        }

        await this.InternalPauseAsync();
    }

    /// <summary>
    /// Pauses the audio asynchronously.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task InternalPauseAsync()
    {
        await this.audioService.PauseAsync();
        this.IsPlaying = false;
    }

    /// <summary>
    /// Internal method which plays audio.
    /// </summary>
    /// <param name="position">Where to start playing from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task InternalPlayAsync(double position = 0)
    {
        await this.audioService.PlayAsync(position);
        this.IsPlaying = true;
    }
}
