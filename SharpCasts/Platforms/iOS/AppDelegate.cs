using Foundation;

namespace SharpCasts;

/// <summary>
/// The class that bootstraps the IOS application.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    /// <summary>
    /// Builds configuration for the application.
    /// </summary>
    /// <returns>The configured application.</returns>
    protected override MauiApp CreateMauiApp()
        => MauiProgram.CreateMauiApp();
}
