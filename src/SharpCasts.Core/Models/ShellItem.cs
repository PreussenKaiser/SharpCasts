namespace SharpCasts.Core.Models;

/// <summary>
/// Represents an item in a <see cref="Shell"/>.
/// </summary>
public class ShellItem
{
    /// <summary>
    /// Gets or initializes the item's title.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes the item's default icon.
    /// </summary>
    public string Icon { get; init; }

    /// <summary>
    /// Gets or initializes the page which the item navigates to.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <br/>
    /// <c>PageType = typeof(MainPage)</c>
    /// </remarks>
    public Type PageType { get; init; }
}
