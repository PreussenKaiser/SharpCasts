using SharpCasts.Views;

namespace SharpCasts;

/// <summary>
/// The class that represents the main enrty-point for the application.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Initializes a new instance of the <see cref="App">App</see> class.
	/// </summary>
	public App()
	{
		this.InitializeComponent();

		this.MainPage = new AppShell();
	}
}
