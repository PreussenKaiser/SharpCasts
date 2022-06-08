namespace SharpCasts.Audio.Platforms.Android.CurrentActivity;

/// <summary>
/// The enum that contains media activity event types.
/// </summary>
public enum ActivityEvent
{
    Created,
    Resumed,
    Paused,
    Destroyed,
    SaveInstanceState,
    Started,
    Stopped
}