using SharpCasts.Main.Models;

namespace SharpCasts;

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

		this.MainPage = new AppShell();
	}

	/// <summary>
	/// Gets or sets the currently logged in user.
	/// </summary>
    public static User CurrentUser { get; set; }
}
