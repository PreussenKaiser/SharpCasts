using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class that represents the settings page.
/// </summary>
public partial class SettingsPage : ContentPage
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SettingsPage"/> content page.
	/// </summary>
	/// <param name="viewmodel">The pages viewmodel.</param>
	public SettingsPage(SettingsPageViewmodel viewmodel)
	{
		this.InitializeComponent();

		this.BindingContext = viewmodel;
	}
}