using Android.Media;
using SharedMauiLib.Platforms.Android.CurrentActivity;

namespace SharedMauiLib.Platforms.Android;

/// <summary>
/// The class which represents Android, audio service.
/// </summary>
public class NativeAudioService : INativeAudioService
{
    /// <summary>
    /// Executes when the current trak is changed.
    /// </summary>
    public event EventHandler<bool> IsPlayingChanged;

    /// <summary>
    /// The current instance of the audio activity.
    /// </summary>
    private IAudioActivity instance;

    /// <summary>
    /// Gets the <see cref="MediaPlayer">Android media player</see>.
    /// </summary>
    private MediaPlayer MediaPlayer
        => this.instance is not null && this.instance.Binder.GetMediaPlayerService() is not null
            ? this.instance.Binder.GetMediaPlayerService().mediaPlayer
            : null;

    /// <summary>
    /// Gets whether the audio service is playing or not.
    /// </summary>
    public bool IsPlaying
        => this.MediaPlayer?.IsPlaying ?? false;

    /// <summary>
    /// Gets the current position of the track.
    /// </summary>
    public double CurrentPosition
        => this.MediaPlayer?.CurrentPosition / 1000 ?? 0;

    /// <summary>
    /// Initializes the native audio serivice asynchronously.
    /// </summary>
    /// <param name="audioURI">The audio to play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task InitializeAsync(string audioURI)
    {
        if (this.instance is null)
        {
            ICurrentActivity activity = CrossCurrentActivity.Current;
            this.instance = activity.Activity as IAudioActivity;
        }
        else
        {
            this.instance.Binder.GetMediaPlayerService().isCurrentEpisode = false;
            this.instance.Binder.GetMediaPlayerService().UpdatePlaybackStateStopped();
        }

        this.instance.Binder.GetMediaPlayerService().PlayingChanged += (object sender, bool e) =>
        {
            Task.Run(async () => {
                if (e)
                {
                    await this.PlayAsync();
                }
                else
                {
                    await this.PauseAsync();
                }
            });

            this.IsPlayingChanged?.Invoke(this, e);
        };

        this.instance.Binder.GetMediaPlayerService().AudioUrl = audioURI;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Pauses the media.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PauseAsync()
    {
        if (this.IsPlaying)
            return this.instance.Binder
                                .GetMediaPlayerService()
                                .Pause();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Plays the track.
    /// </summary>
    /// <param name="position">Where in the track to play from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task PlayAsync(double position = 0)
    {
        await this.instance.Binder
                           .GetMediaPlayerService()
                           .Play();

        await this.instance.Binder
                           .GetMediaPlayerService()
                           .Seek((int)position * 1000);
    }

    /// <summary>
    /// Mutes the media.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetMuted(bool value)
    {
        this.instance?.Binder
                      .GetMediaPlayerService()
                      .SetMuted(value);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets the track's volumen.
    /// </summary>
    /// <param name="value">The volme to set to.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetVolume(int value)
    {
        this.instance?.Binder
                      .GetMediaPlayerService()
                      .SetVolume(value);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Task SetCurrentTime(double position)
        => this.instance.Binder
                        .GetMediaPlayerService()
                        .Seek((int)position * 1000);

    /// <summary>
    /// Disposes the <see cref="NativeAudioService"/> class.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public ValueTask DisposeAsync()
    {
        this.instance.Binder?.Dispose();

        return ValueTask.CompletedTask;
    }
}
