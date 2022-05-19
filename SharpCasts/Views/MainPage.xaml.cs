namespace SharpCasts.Views;

/// <summary>
/// The class which represents the main page.
/// </summary>
public partial class MainPage : ContentPage
{
	/// <summary>
	/// 
	/// </summary>
	private int count;

	/// <summary>
	/// Initializes a new instance of the <see cref="MainPage">MainPage</see> content page.
	/// </summary>
	public MainPage()
	{
		this.InitializeComponent();

		this.count = 0;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnCounterClicked(object sender, EventArgs e)
	{
		this.count++;

		if (this.count == 1)
			this.CounterBtn.Text = $"Clicked {this.count} time";
		else
			this.CounterBtn.Text = $"Clicked {this.count} times";

		SemanticScreenReader.Announce(this.CounterBtn.Text);
	}
}

