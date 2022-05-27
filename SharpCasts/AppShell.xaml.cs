using SharpCasts.Main.Views;

namespace SharpCasts;

/// <summary>
/// The class which represents the app's shell.
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppShell">AppShell</see> class.
    /// </summary>
    public AppShell()
    {
        this.InitializeComponent();

        // Routes in tabbed navigation.
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(DiscoverPage), typeof(DiscoverPage));
        Routing.RegisterRoute(nameof(PodcastPage), typeof(PodcastPage));

        // Other routes.
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}
