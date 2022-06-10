using Android.App;

namespace SharpCasts.Audio.Platforms.Android.CurrentActivity;

/// <summary>
/// 
/// </summary>
public class ActivityEventArgs : EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="ev"></param>
    internal ActivityEventArgs(Activity activity, ActivityEvent ev)
    {
        this.Activity = activity;
        this.Event = ev;
    }

    /// <summary>
    /// 
    /// </summary>
    public Activity Activity { get; }

    /// <summary>
    /// 
    /// </summary>
    public ActivityEvent Event { get; }
}