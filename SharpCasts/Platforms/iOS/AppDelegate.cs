using Foundation;

namespace SharpCasts;

/// <summary>
/// 
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
