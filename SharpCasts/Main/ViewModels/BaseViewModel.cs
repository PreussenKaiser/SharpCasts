using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The class that represents the base viewmodel.
/// </summary>
public partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Whether the page is busy or not.
    /// </summary>
    [ObservableProperty]
    private bool isBusy;

    /// <summary>
    /// The title of the page.
    /// </summary>
    [ObservableProperty]
    private string title;
}
