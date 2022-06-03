namespace SharedMauiLib.Platforms.Android;

/// <summary>
/// 
/// </summary>
public interface IAudioActivity
{
    /// <summary>
    /// 
    /// </summary>
    public event StatusChangedEventHandler StatusChanged;

    /// <summary>
    /// 
    /// </summary>
    public event CoverReloadedEventHandler CoverReloaded;

    /// <summary>
    /// 
    /// </summary>
    public event PlayingEventHandler Playing;

    /// <summary>
    /// 
    /// </summary>
    public event BufferingEventHandler Buffering;

    /// <summary>
    /// 
    /// </summary>
    public MediaPlayerServiceBinder Binder { get; set; }
}
