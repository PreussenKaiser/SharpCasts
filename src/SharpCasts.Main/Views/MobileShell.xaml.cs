using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the MobileShell view.
/// </summary>
public partial class MobileShell : Shell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MobileShell">AppShell</see> class.
    /// </summary>
    public MobileShell()
    {
        this.InitializeComponent();

        this.BindingContext = new ShellViewModel();
    }
}
