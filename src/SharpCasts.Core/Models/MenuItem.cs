namespace SharpCasts.Core.Models;

/// <summary>
/// Represents a menu itme.
/// </summary>
/// <remarks>
/// Was created to encapsulate the behavior of a navigation item in a <see cref="CollectionView"/>.
/// </remarks>
public class MenuItem
{
    /// <summary>
    /// Gets or initializes the menu item's icon.
    /// </summary>
    public string Icon { get; init; }

    /// <summary>
    /// Gets or initializes the menu item's name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets or initializes where the menu item will navigate to.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <br/>
    /// <c>Route = nameof(SettingsPage)</c>
    /// </remarks>
    public string Route { get; init; }
}
