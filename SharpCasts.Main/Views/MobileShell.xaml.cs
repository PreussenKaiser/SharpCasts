using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the mobile application's shell.
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
