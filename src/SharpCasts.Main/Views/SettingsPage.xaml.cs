using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// Code behind for the SettingsPage view.
/// </summary>
public partial class SettingsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    /// <param name="viewModel">The pages view model.</param>
    public SettingsPage(SettingsPageViewModel viewModel)
	{
		this.InitializeComponent();

		this.BindingContext = viewModel;
	}
}