using SharpCasts.Main.Views;

namespace SharpCasts.Main;

/// <summary>
/// The class that represents the main entry-point for the application.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Initializes a new instance of the <see cref="App"/> class.
	/// </summary>
	public App()
	{
		this.InitializeComponent();

		this.MainPage = new MobileShell();

        // Routes in tabbed navigation.
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(DiscoverPage), typeof(DiscoverPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));

        // User routes.
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        // Podcast routes.
        Routing.RegisterRoute(nameof(PodcastPage), typeof(PodcastPage));

        // Profile routes.
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
