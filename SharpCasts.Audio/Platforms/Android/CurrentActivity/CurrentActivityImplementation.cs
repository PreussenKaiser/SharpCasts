using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidApp = Android.App;
using Application = Android.App.Application;

namespace SharpCasts.Audio.Platforms.Android.CurrentActivity;

/// <summary>
/// Implementation for Feature.
/// </summary>
[Preserve(AllMembers = true)]
public class CurrentActivityImplementation : ICurrentActivity
{
    /// <summary>
    /// Activity state changed event handler.
    /// </summary>
    public event EventHandler<ActivityEventArgs> ActivityStateChanged;

    /// <summary>
    /// 
    /// </summary>
    private ActivityLifecycleContextListener lifecycleListener;

    /// <summary>
    /// Gets or sets the activity.
    /// </summary>
    /// <value>The activity.</value>
    public Activity Activity
	{
		get => this.lifecycleListener?.Activity;
		set
		{
			if (this.lifecycleListener is null)
				this.Init(value, null);
		}
	}

    /// <summary>
    /// Gets the current application context
    /// </summary>
    public Context AppContext =>
        Application.Context;

    /// <summary>
    /// Waits for an activity to be ready.
    /// </summary>
    /// <returns></returns>
    public async Task<Activity> WaitForActivityAsync(CancellationToken cancelToken = default)
	{
		if (this.Activity is not null)
			return this.Activity;

        TaskCompletionSource<Activity> tcs = new();

		var handler = new EventHandler<ActivityEventArgs>((sender, args) =>
		{
			if (args.Event == ActivityEvent.Created 
				|| args.Event == ActivityEvent.Resumed)
            {
				tcs.TrySetResult(args.Activity);
            }
		});

		try
		{
			using (cancelToken.Register(() => tcs.TrySetCanceled()))
			{
				this.ActivityStateChanged += handler;
				return await tcs.Task.ConfigureAwait(false);
			}
		}
		finally
		{
			this.ActivityStateChanged -= handler;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="ev"></param>
	internal void RaiseStateChanged(Activity activity, ActivityEvent ev)
		=> this.ActivityStateChanged?.Invoke(this, new ActivityEventArgs(activity, ev));

	/// <summary>
	/// Initialize current activity with application
	/// </summary>
	/// <param name="application">The main application</param>
	public void Init(AndroidApp.Application application)
	{
		if (this.lifecycleListener is not null)
			return;

		this.lifecycleListener = new ActivityLifecycleContextListener();
		application.RegisterActivityLifecycleCallbacks(lifecycleListener);
	}

	/// <summary>
	/// Initialize current activity with activity!
	/// </summary>
	/// <param name="activity">The main activity</param>
	/// <param name="bundle">Bundle for activity </param>
	public void Init(Activity activity, Bundle bundle)
	{
		this.Init(activity.Application);
		this.lifecycleListener.Activity = activity;
	}
}

/// <summary>
/// 
/// </summary>
[Preserve(AllMembers = true)]
public class ActivityLifecycleContextListener : Java.Lang.Object, Application.IActivityLifecycleCallbacks
{
	/// <summary>
	/// 
	/// </summary>
	private WeakReference<Activity> currentActivity;

	/// <summary>
	/// Initializes a new instance of the <see cref="ActivityLifecycleContextListener"/> class.
	/// </summary>
    public ActivityLifecycleContextListener()
		=> this.currentActivity = new WeakReference<Activity>(null);

	/// <summary>
	/// 
	/// </summary>
	public Context Context =>
		this.Activity ?? Application.Context;

	/// <summary>
	/// 
	/// </summary>
	public Activity Activity
	{
		get => this.currentActivity.TryGetTarget(out var a)
				? a
				: null;

		set => this.currentActivity.SetTarget(value);
	}

	/// <summary>
	/// 
	/// </summary>
	private CurrentActivityImplementation Current =>
		(CurrentActivityImplementation)(CrossCurrentActivity.Current);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="savedInstanceState"></param>
	public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
	{
		this.Activity = activity;
		this.Current.RaiseStateChanged(activity, ActivityEvent.Created);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	public void OnActivityDestroyed(Activity activity)
		=> this.Current.RaiseStateChanged(activity, ActivityEvent.Destroyed);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	public void OnActivityPaused(Activity activity)
	{
		this.Activity = activity;
		this.Current.RaiseStateChanged(activity, ActivityEvent.Paused);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	public void OnActivityResumed(Activity activity)
	{
		this.Activity = activity;
		this.Current.RaiseStateChanged(activity, ActivityEvent.Resumed);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="outState"></param>
	public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
		=> this.Current.RaiseStateChanged(activity, ActivityEvent.SaveInstanceState);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	public void OnActivityStarted(Activity activity)
		=> this.Current.RaiseStateChanged(activity, ActivityEvent.Started);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	public void OnActivityStopped(Activity activity)
		=> this.Current.RaiseStateChanged(activity, ActivityEvent.Stopped);
}