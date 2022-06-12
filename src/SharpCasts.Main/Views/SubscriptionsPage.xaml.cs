using SharpCasts.Main.ViewModels;

namespace SharpCasts.Main.Views;

/// <summary>
/// The class which represents the subscriptions page.
/// </summary>
public partial class SubscriptionsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionsPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the subscriptions page.</param>
    public SubscriptionsPage(SubscriptionsPageViewModel viewModel)
    {
		this.InitializeComponent();

		this.BindingContext = viewModel;
    }
}

