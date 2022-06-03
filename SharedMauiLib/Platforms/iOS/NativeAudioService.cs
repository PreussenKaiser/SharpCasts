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
    /// 
    /// </summary>
    /// <param name="audioURI"></param>
    /// <returns></returns>
    public async Task InitializeAsync(string audioURI)
    {
        this.uri = audioURI;
        NSUrl fileURL = new NSUrl(uri.ToString());

        if (avPlayer != null)
        {
            await this.PauseAsync();
        }

        avPlayer = new AVPlayer(fileURL);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task PauseAsync()
    {
        this.avPlayer?.Pause();

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
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
    {
        return this.avPlayer.SeekAsync(new CoreMedia.CMTime((long)value, 1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetMuted(bool value)
    {
        if (this.avPlayer != null)
        {
            this.avPlayer.Muted = value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetVolume(int value)
    {
        if (this.avPlayer != null)
        {
            this.avPlayer.Volume = value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask DisposeAsync()
    {
        this.avPlayer?.Dispose();
        return ValueTask.CompletedTask;
    }
}