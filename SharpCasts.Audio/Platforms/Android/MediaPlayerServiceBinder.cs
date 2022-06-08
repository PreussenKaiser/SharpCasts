using Android.OS;

namespace SharpCasts.Audio.Platforms.Android;

/// <summary>
/// 
/// </summary>
public class MediaPlayerServiceBinder : Binder
{
    /// <summary>
    /// 
    /// </summary>
    private readonly MediaPlayerService service;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaPlayerServiceBinder"/> class.
    /// </summary>
    /// <param name="service"></param>
    public MediaPlayerServiceBinder(MediaPlayerService service)
        => this.service = service;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public MediaPlayerService GetMediaPlayerService()
        => this.service;
}
