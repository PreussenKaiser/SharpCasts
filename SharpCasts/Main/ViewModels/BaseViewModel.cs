using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The class that represents the base viewmodel.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Whether the page is busy or not.
    /// </summary>
    private bool isBusy;

    /// <summary>
    /// Gets or sets the title of the page.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets whether the page is busy or not.
    /// </summary>
    /// <remarks>
    /// Usages could be:
    /// <list type="bullet">
    ///     <item>
    ///     is the page refreshing?
    ///     </item>
    ///     <item>
    ///     is the expander expanded?
    ///     </item>
    /// </list>
    /// </remarks>
    public bool IsBusy
    {
        get => this.isBusy;
        set
        {
            this.isBusy = value;
            this.OnPropertyChanged(nameof(this.IsBusy));
        }
    }

    /// <summary>
    /// Raises the property changed event.
    /// </summary>
    /// <param name="propertyName">The properties name.</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
