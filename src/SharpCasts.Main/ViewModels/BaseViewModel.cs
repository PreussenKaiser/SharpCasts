using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The class that represents the base view model.
/// </summary>
public partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Whether the page is busy or not.
    /// </summary>
    [ObservableProperty]
    private bool isBusy;
}
