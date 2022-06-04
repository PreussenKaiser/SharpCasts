using AVFoundation;
using Foundation;

namespace SharedMauiLib.Platforms.iOS;

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
    private AVPlayer avPlayer;

    /// <summary>
    /// 
    /// </summary>
    private string uri;

    /// <summary>
    /// 
    /// </summary>
    public bool IsPlaying => avPlayer != null
        ? avPlayer.TimeControlStatus == AVPlayerTimeControlStatus.Playing
        : false;

    /// <summary>
    /// 
    /// </summary>
    public double CurrentPosition => avPlayer?.CurrentTime.Seconds ?? 0;

    /// <summary>
    /// Initializes the audio service.
    /// </summary>
    /// <param name="audioURI">The url to play audio from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task InitializeAsync(string audioURI)
    {
        this.uri = audioURI;
        NSUrl fileURL = new(this.uri.ToString());

        if (this.avPlayer is not null)
        {
            await this.PauseAsync();
        }

        this.avPlayer = new AVPlayer(fileURL);
    }

    /// <summary>
    /// Pauses the audio service.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PauseAsync()
    {
        this.avPlayer?.Pause();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Plays the audio asynchronously.
    /// </summary>
    /// <param name="position">Where to start the track from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task PlayAsync(double position = 0)
    {
        await this.avPlayer.SeekAsync(new CoreMedia.CMTime((long)position, 1));
        this.avPlayer?.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetCurrentTime(double value)
        => this.avPlayer.SeekAsync(new CoreMedia.CMTime((long)value, 1));

    /// <summary>
    /// Mutes the track.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetMuted(bool value)
    {
        if (this.avPlayer is not null)
        {
            this.avPlayer.Muted = value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets the track's volume.
    /// </summary>
    /// <param name="value">The volume to set the audio to.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetVolume(int value)
    {
        if (this.avPlayer is not null)
        {
            this.avPlayer.Volume = value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Disposes the native audio service asynchronously.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public ValueTask DisposeAsync()
    {
        this.avPlayer?.Dispose();

        return ValueTask.CompletedTask;
    }
}