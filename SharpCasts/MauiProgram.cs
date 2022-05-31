using SharpCasts.Main.Services.Podcasts;
using SharpCasts.Main.Services.Subscriptions;
using SharpCasts.Main.Services.Users;
using SharpCasts.Main.ViewModels;
using SharpCasts.Main.Views;

using System.Reflection;

using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui;

namespace SharpCasts;

/// <summary>
/// The class that initializes the application.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// The path to the configuration file.
    /// </summary>
    private const string CONFIG_PATH = "SharpCasts.appsettings.json";

    /// <summary>
    /// Loads application configuration.
    /// </summary>
    /// <returns>The configured application.</returns>
    public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
				     .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        LoadServices(ref builder);
        LoadPreferences();

		return builder.Build();
	}

    /// <summary>
    /// Sets application preferences.
    /// </summary>
	private static void LoadPreferences()
    {
        var config = BuildConfig();

        // Api settings.
        Preferences.Set("Endpoint", config["Api:Endpoint"]);
        Preferences.Set("ApiKey", config["Api:Key"]);
        Preferences.Set("ApiSecret", config["Api:Secret"]);
        Preferences.Set("AccessToken", config["Api:AccessToken"]);

        // Database settings.
        Preferences.Set("Source", config["Database:Source"]);
        Preferences.Set("InitialCatalog", config["InitialCatalog"]);
        Preferences.Set("UserID", config["UserID"]);
        Preferences.Set("Password", config["Password"]);
    }

    /// <summary>
    /// Loads application services.
    /// </summary>
    /// <param name="builder">The app builder to load services to.</param>
    private static void LoadServices(ref MauiAppBuilder builder)
    {
        // Register views.
        builder.Services.AddTransient<MainPage>()
                        .AddTransient<DiscoverPage>()
                        .AddTransient<PodcastPage>()
                        .AddTransient<LoginPage>()
                        .AddTransient<RegisterPage>()
                        .AddTransient<ProfilePage>();

        // Register viewmodels.
        builder.Services.AddSingleton<MainPageViewmodel>()
                        .AddSingleton<DiscoverPageViewmodel>()
                        .AddTransient<PodcastPageViewmodel>()
                        .AddSingleton<LoginPageViewmodel>()
                        .AddSingleton<RegisterPageViewmodel>()
                        .AddSingleton<ProfilePageViewmodel>();

        // Register services.
        builder.Services.AddSingleton<IPodcastService, PodcastService>()
                        .AddSingleton<IUserService, UserService>()
                        .AddSingleton<ISubscriptionService, SubscriptionService>();
    }

    /// <summary>
    /// Builds application configuration.
    /// </summary>
    /// <returns>Configuration settings from config.json</returns>
	private static IConfiguration BuildConfig()
    {
        using var stream = Assembly
                            .GetExecutingAssembly()
                            .GetManifestResourceStream(CONFIG_PATH);

        return new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
    }
}
