namespace SharpCasts.Audio.Platforms.Android;

/// <summary>
/// The interface which implements audio activity events. 
/// </summary>
public interface IAudioActivity
{
    /// <summary>
    /// Executes when the audio activity's status has changed.
    /// </summary>
    public event StatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Executes when the cover image is reloaded.
    /// </summary>
    public event CoverReloadedEventHandler CoverReloaded;

    /// <summary>
    /// Executes when the audio starts playing.
    /// </summary>
    public event PlayingEventHandler Playing;

    /// <summary>
    /// Executes when the audio starts buffering.
    /// </summary>
    public event BufferingEventHandler Buffering;

    /// <summary>
    /// 
    /// </summary>
    public MediaPlayerServiceBinder Binder { get; set; }
}
