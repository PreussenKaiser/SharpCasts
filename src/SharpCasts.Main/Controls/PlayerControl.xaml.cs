using SharpCasts.Core.Services;

namespace SharpCasts.Main.Controls;

/// <summary>
/// Represents an audio player.
/// </summary>
public partial class PlayerControl : ContentView
{
    /// <summary>
    /// The service to play media with.
    /// </summary>
    private readonly IPlayerService playerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerControl"/> class.
    /// </summary>
	public PlayerControl()
    {
        this.playerService = this.Handler.MauiContext.Services.GetService<IPlayerService>();
        this.IsVisible = false;

#if ANDROID
        this.HeightRequest = 70;
#elif WINDOWS
        this.HeightRequest = 90;
#endif

        this.InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PlayButton_Clicked(object sender, EventArgs e)
    {
        await this.playerService.PlayAsync(this.playerService.CurrentEpisode);
    }
}