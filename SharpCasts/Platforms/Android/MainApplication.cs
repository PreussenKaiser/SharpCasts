using Android.App;
using Android.Runtime;

namespace SharpCasts;

/// <summary>
/// The class that represents the main entry-point of the Android application.
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainApplication"/> class.
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="ownership"></param>
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

    /// <summary>
    /// Builds configuration for the application.
    /// </summary>
    /// <returns>The configured application.</returns>
    protected override MauiApp CreateMauiApp()
        => MauiProgram.CreateMauiApp();
}
