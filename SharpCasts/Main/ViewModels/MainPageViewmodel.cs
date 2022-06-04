﻿using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Services.Subscriptions;
using SharpCasts.Main.Views;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="MainPage"/> content page.
/// </summary>
public partial class MainPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get subscribed podcasts with.
    /// </summary>
    private readonly ISubscriptionService subscribedService;

    /// <summary>
    /// The service to get podcasts with.
    /// </summary>
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPageViewmodel"/> class.
    /// </summary>
    /// <param name="subscribedService">The service to get subscribed podcasts with.</param>
    /// <param name="podcastService">The service to get podcasts with.</param>
    public MainPageViewmodel(ISubscriptionService subscribedService,
                             IPodcastService podcastService)
    {
        this.subscribedService = subscribedService;
        this.podcastService = podcastService;

        this.RefreshCommand = new Command(this.Refresh);
    }

    /// <summary>
    /// Gets the command to execute when the page refreshes.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Refreshes the home page.
    /// </summary>
    private void Refresh()
    {
        this.IsBusy = true;
        this.IsBusy = false;
    }
}
