using Android.Media;
using SharedMauiLib.Platforms.Android.CurrentActivity;

namespace SharedMauiLib.Platforms.Android;

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
    private IAudioActivity instance;

    /// <summary>
    /// 
    /// </summary>
    private MediaPlayer MediaPlayer => this.instance != null &&
        this.instance.Binder.GetMediaPlayerService() != null ?
        this.instance.Binder.GetMediaPlayerService().mediaPlayer : null;

    /// <summary>
    /// 
    /// </summary>
    public bool IsPlaying
        => this.MediaPlayer?.IsPlaying ?? false;

    /// <summary>
    /// 
    /// </summary>
    public double CurrentPosition
        => this.MediaPlayer?.CurrentPosition / 1000 ?? 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="audioURI"></param>
    /// <returns></returns>
    public Task InitializeAsync(string audioURI)
    {
        if (this.instance == null)
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
    /// 
    /// </summary>
    /// <returns></returns>
    public Task PauseAsync()
    {
        if (this.IsPlaying)
            return this.instance.Binder
                                .GetMediaPlayerService()
                                .Pause();

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetMuted(bool value)
    {
        this.instance?.Binder
                      .GetMediaPlayerService()
                      .SetMuted(value);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask DisposeAsync()
    {
        this.instance.Binder?.Dispose();

        return ValueTask.CompletedTask;
    }
}
