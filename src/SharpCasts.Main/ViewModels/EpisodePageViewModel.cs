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
    /// Whether audio is playing or not.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(IsNotPlaying))]
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
        init
        {
            this.SetProperty(ref this.episode, value);

            this.IsPlaying = this.playerService.IsPlaying
                && this.playerService.CurrentEpisode?.Id == this.Episode.Id;
        } 
    }
        

    /// <summary>
    /// Gets whether an episode isn't playing or not.
    /// </summary>
    public bool IsNotPlaying
        => !this.IsPlaying;

    /// <summary>
    /// Plays the current episode.
    /// </summary>
    [ICommand]
    private async void PlayAsync()
    {
        await this.playerService.PlayAsync(this.Episode);
        this.IsPlaying = true;
    }

    /// <summary>
    /// Pauses the current episode.
    /// </summary>
    [ICommand]
    private async void PauseAsync()
    {
        if (!this.IsPlaying)
            return;

        await this.playerService.PauseAsync(this.Episode);
        this.IsPlaying = false;
    }
}
