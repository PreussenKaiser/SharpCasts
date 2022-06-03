﻿using SharpCasts.Core.Contexts;
using SharpCasts.Main.Services.Players;
using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Services.Subscriptions;
using SharpCasts.Main.Services.Users;
using SharpCasts.Main.ViewModels;
using SharpCasts.Main.Views;

namespace SharpCasts.Core.Extensions.Builders;

/// <summary>
/// The class which contains <see cref="MauiAppBuilder"/> extension methods.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Loads views into the app builder.
    /// </summary>
    /// <param name="builder">The builder to load views into.</param>
    /// <returns>The builder with initializes view.s</returns>
    public static MauiAppBuilder LoadViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainPage>()
                        .AddTransient<DiscoverPage>()
                        .AddTransient<PodcastPage>()
                        .AddTransient<LoginPage>()
                        .AddTransient<RegisterPage>()
                        .AddTransient<ProfilePage>();

        return builder;
    }

    /// <summary>
    /// Loads viewmodels into the app builder.
    /// </summary>
    /// <param name="builder">The app builder to load views into.</param>
    /// <returns>The builder with initialized viewmodels.</returns>
    public static MauiAppBuilder LoadViewmodels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewmodel>()
                        .AddSingleton<DiscoverPageViewmodel>()
                        .AddTransient<PodcastPageViewmodel>()
                        .AddSingleton<LoginPageViewmodel>()
                        .AddSingleton<RegisterPageViewmodel>()
                        .AddSingleton<ProfilePageViewmodel>();

        return builder;
    }

    /// <summary>
    /// Loads services into the app builder.
    /// </summary>
    /// <param name="builder">The app builder to load services into.</param>
    /// <returns>The builder with initialized services.</returns>
    public static MauiAppBuilder LoadServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IPodcastService, PodcastService>()
                        .AddSingleton<IUserService, UserService>()
                        .AddSingleton<ISubscriptionService, SubscriptionService>()
                        .AddSingleton<IPlayerService, PlayerService>()
                        .AddSingleton<SharedMauiLib.INativeAudioService, SharedMauiLib.Platforms.Android.NativeAudioService>()
                        .AddDbContext<PodcastContext>();

        return builder;
    }
}