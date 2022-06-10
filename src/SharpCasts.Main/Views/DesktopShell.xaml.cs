using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class that represents the dekstop application's shell.
/// </summary>
public partial class DesktopShell : Shell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DesktopShell"/> class.
    /// </summary>
	public DesktopShell()
	{
		this.InitializeComponent();

        this.BindingContext = new ShellViewModel();
	}
}