namespace SharpCasts.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// <br/>
/// To learn more about WinUI, the WinUI project structure,
/// and more about our project templates, see: <see href="http://aka.ms/winui-project-info"/>.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
        => this.InitializeComponent();

    /// <summary>
    /// Builds configuration for the application.
    /// </summary>
    /// <returns>The configured application.</returns>
    protected override MauiApp CreateMauiApp()
        => MauiProgram.CreateMauiApp();
}

