using SharpCasts.Main.Views;
using ShellItem = SharpCasts.Core.Models.ShellItem;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for application shells.
/// </summary>
/// <remarks>
/// Shells include:
/// <list type="bullet">
///     <item><see cref="MobileShell"/></item>
///     <item><see cref="DesktopShell"/></item>
/// </list>
/// </remarks>
public partial class ShellViewModel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShellViewModel"/> class;
    /// </summary>
    public ShellViewModel()
    {
        this.Subscriptions = new ShellItem
        {
            Title = "Subscriptions",
            Icon = "subscriptions.png",
            PageType = typeof(SubscriptionsPage)
        };

        this.Discover = new ShellItem
        {
            Title = "Discover",
            Icon = "discover.png",
            PageType = typeof(DiscoverPage)
        };

        this.Profile = new ShellItem
        {
            Title = "Profile",
            Icon = "profile.png",
            PageType = typeof(ProfilePage)
        };
    }

    /// <summary>
    /// Gets the home shell item.
    /// </summary>
    public ShellItem Subscriptions { get; }

    /// <summary>
    /// Gets the discover shell item.
    /// </summary>
    public ShellItem Discover { get; }

    /// <summary>
    /// Gets the profile shell item.
    /// </summary>
    public ShellItem Profile { get; }
}
