using AVFoundation;
using Foundation;

namespace SharedMauiLib.Platforms.MacCatalyst;

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
    private string url;

    /// <summary>
    /// Gets whether or not audio is playing.
    /// </summary>
    public bool IsPlaying
        => this.avPlayer is not null
        ? this.avPlayer.TimeControlStatus == AVPlayerTimeControlStatus.Playing
        : false;
    
    /// <summary>
    /// Gets the audio player's current position in the track.
    /// </summary>
    public double CurrentPosition
        => this.avPlayer?.CurrentTime.Seconds ?? 0;

    /// <summary>
    /// Initializes the MacOS audio service asynchronously.
    /// </summary>
    /// <param name="audioURI">The url of the audio to play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task InitializeAsync(string audioURI)
    {
        this.url = audioURI;
        NSUrl fileURL = new(this.url.ToString());

        if (this.avPlayer is not null)
        {
            await this.PauseAsync();
        }

        this.avPlayer = new AVPlayer(fileURL);
    }

    /// <summary>
    /// Pauses the audio.
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
    /// <param name="position">The position to play the audio from.</param>
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
    /// <returns></returns>
    public Task SetCurrentTime(double value)
        => this.avPlayer.SeekAsync(new CoreMedia.CMTime((long)value, 1));

    /// <summary>
    /// Mutes the currently playing audio.
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
    /// Sets the audio's volume.
    /// </summary>
    /// <param name="value">The volume to set.</param>
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
    /// Disposes the MacOS audio service.
    /// </summary>
    /// <returns>Whether the task ws completed or not.</returns>
    public ValueTask DisposeAsync()
    {
        this.avPlayer?.Dispose();

        return ValueTask.CompletedTask;
    }
}
