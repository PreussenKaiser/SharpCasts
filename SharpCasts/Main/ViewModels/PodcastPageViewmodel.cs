using SharpCasts.Main.Models;
using SharpCasts.Main.Views;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="PodcastPage">PodcastPage/> content page.
/// </summary>
public class PodcastPageViewmodel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastPageViewmodel">PodcastPageViewmodel</see> viewmodel.
    /// </summary>
    public PodcastPageViewmodel()
    {
    }

    /// <summary>
    /// Gets the podcast to display.
    /// </summary>
    public Podcast Podcast { get; set; }
}
