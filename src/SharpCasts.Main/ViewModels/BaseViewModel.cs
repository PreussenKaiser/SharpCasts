using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// Represents the base view model.
/// </summary>
public partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Whether the page is busy or not.
    /// </summary>
    [ObservableProperty]
    private bool isBusy;
}
