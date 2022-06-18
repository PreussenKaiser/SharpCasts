using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for <see cref="EpisodePage"/>.
/// </summary>
[QueryProperty(nameof(Episode), nameof(Episode))]
public partial class EpisodePageViewModel : BaseViewModel
{
    /// <summary>
    /// The service to play audio with.
    /// </summary>
    private readonly IPlayerService playerService;

    /// <summary>
    /// The episode to display.
    /// </summary>
    private Episode episode;

    /// <summary>
    /// Whether the episode is playing or not.
    /// </summary>
    [ObservableProperty]
    private bool isPlaying;

    /// <summary>
    /// Initializes a new instance of the <see cref="EpisodePageViewModel"/> class.
    /// </summary>
    /// <param name="playerService">The service to play audio with.</param>
    public EpisodePageViewModel(IPlayerService playerService)
        => this.playerService = playerService;

    /// <summary>
    /// Gets or sets the episode to display.
    /// </summary>
    public Episode Episode
    {
        get => this.episode;
        init => this.SetProperty(ref this.episode, value);
    }

    /// <summary>
    /// Plays the currently selected episode.
    /// Called when the user opts to play an episode.
    /// </summary>
    /// <param name="episode">The episode to play.</param>
    [ICommand]
    private async void PlayAsync(Episode episode)
        => await this.playerService.PlayAsync(episode);
}
