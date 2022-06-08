using Windows.Media.Core;
using Windows.Media.Playback;

namespace SharpCasts.Audio.Platforms.Windows;

/// <summary>
/// 
/// </summary>
public class NativeAudioService : INativeAudioService
{
    /// <summary>
    /// 
    /// </summary>
    public event EventHandler<bool> IsPlayingChanged;

    /// <summary>
    /// 
    /// </summary>
    private MediaPlayer mediaPlayer;

    /// <summary>
    /// 
    /// </summary>
    private string uri;

    /// <summary>
    /// Gets whether the audio player is playing or not.
    /// </summary>
    public bool IsPlaying
        => this.mediaPlayer is not null
            && this.mediaPlayer.CurrentState == MediaPlayerState.Playing;

    /// <summary>
    /// Gets the current position of the track.
    /// </summary>
    public double CurrentPosition
        => this.mediaPlayer?.Position.TotalSeconds ?? 0;

    /// <summary>
    /// Initializes the Windows audio service asynchronously.
    /// </summary>
    /// <param name="audioURI">The url of the audio to play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task InitializeAsync(string audioURI)
    {
        this.uri = audioURI;

        if (this.mediaPlayer is null)
        {
            this.mediaPlayer = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri(uri)),
                AudioCategory = MediaPlayerAudioCategory.Media
            };
        }

        if (this.mediaPlayer is not null)
        {
            await this.PauseAsync();
            this.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(uri));
        }
    }

    /// <summary>
    /// Pauses the currently playing audio.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PauseAsync()
    {
        this.mediaPlayer?.Pause();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Plays audio from the media player asynchronously.
    /// </summary>
    /// <param name="position">Where to start playing the track from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PlayAsync(double position = 0)
    {
        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Position = TimeSpan.FromSeconds(position);
            this.mediaPlayer.Play();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetCurrentTime(double value)
    {
        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Position = TimeSpan.FromSeconds(value);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Mutes currently playing audio.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetMuted(bool value)
    {
        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.IsMuted = value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets the volume of the currently playing audio.
    /// </summary>
    /// <param name="value">The volume to set.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetVolume(int value)
    {
        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Volume = value != 0
                ? value / 100d
                : 0;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Disposes the Windows audio service.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public ValueTask DisposeAsync()
    {
        this.mediaPlayer?.Dispose();

        return ValueTask.CompletedTask;
    }
}
