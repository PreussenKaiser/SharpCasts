using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the DesktopShell view.
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