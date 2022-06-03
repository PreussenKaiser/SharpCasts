using Android.Content;
using Android.OS;

namespace SharedMauiLib.Platforms.Android;

/// <summary>
/// 
/// </summary>
public class MediaPlayerServiceConnection : Java.Lang.Object, IServiceConnection
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IAudioActivity instance;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaPlayerServiceConnection"/> class.
    /// </summary>
    /// <param name="mediaPlayer"></param>
    public MediaPlayerServiceConnection(IAudioActivity mediaPlayer)
        => this.instance = mediaPlayer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="service"></param>
    public void OnServiceConnected(ComponentName name, IBinder service)
    {
        if (service is MediaPlayerServiceBinder binder)
        {
            instance.Binder = binder;

            var mediaPlayerService = binder.GetMediaPlayerService();
            //mediaPlayerService.CoverReloaded += (object sender, EventArgs e) => { instance.CoverReloaded?.Invoke(sender, e); };
            //mediaPlayerService.StatusChanged += (object sender, EventArgs e) => { instance.StatusChanged?.Invoke(sender, e); };
            //mediaPlayerService.Playing += (object sender, EventArgs e) => { instance.Playing?.Invoke(sender, e); };
            //mediaPlayerService.Buffering += (object sender, EventArgs e) => { instance.Buffering?.Invoke(sender, e); };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void OnServiceDisconnected(ComponentName name)
    {
    }
}
