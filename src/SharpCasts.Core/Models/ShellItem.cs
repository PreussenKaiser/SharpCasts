namespace SharpCasts.Core.Models;

/// <summary>
/// The class that represents an item in the application's shell.
/// </summary>
public class ShellItem
{
    /// <summary>
    /// Gets or initializes the item's title.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <br/>
    /// <c>Title = "Home"</c>
    /// </remarks>
    public string Title { get; init; }

    /// <summary>
    /// Gets or initializes the item's default icon.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <br/>
    /// <c>Icon = "subscriptions.png"</c>
    /// </remarks>
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
