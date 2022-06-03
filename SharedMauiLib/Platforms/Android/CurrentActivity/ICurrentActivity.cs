using Android.Content;
using Android.OS;
using AndroidApp = Android.App; 

namespace SharedMauiLib.Platforms.Android.CurrentActivity;

/// <summary>
/// Current Activity Interface
/// </summary>
public interface ICurrentActivity
{
    /// <summary>
    /// Fires when activity state events are fired
    /// </summary>
    public event EventHandler<ActivityEventArgs> ActivityStateChanged;

    /// <summary>
    /// Gets or sets the activity.
    /// </summary>
    /// <value>The activity.</value>
    public AndroidApp.Activity Activity { get; set; }

	/// <summary>
	/// Gets the current Application Context.
	/// </summary>
	/// <value>The app context.</value>
	public Context AppContext { get; }

	/// <summary>
	/// Waits for an activity to be ready for use
	/// </summary>
	/// <returns></returns>
	public Task<AndroidApp.Activity> WaitForActivityAsync(CancellationToken cancelToken = default);

	/// <summary>
	/// Initialize Current Activity Plugin with Application
	/// </summary>
	/// <param name="application"></param>
	public void Init(AndroidApp.Application application);

	/// <summary>
	/// Initialize the current activity with activity and bundle
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="bundle"></param>
	public void Init(AndroidApp.Activity activity, Bundle bundle);
}