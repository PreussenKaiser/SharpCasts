using Microsoft.Extensions.Configuration;
using SharpCasts.Main.Services;
using SharpCasts.Main.ViewModels;
using SharpCasts.Main.Views;
using System.Reflection;

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
        LoadPreferences();
		MauiAppBuilder builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
				     .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // Register views.
        builder.Services.AddTransient<MainPage>()
                        .AddTransient<DiscoverPage>();

        // Register viewmodels.
        builder.Services.AddSingleton<MainPageViewmodel>()
                        .AddSingleton<DiscoverPageViewmodel>();

        // Register services.
        builder.Services.AddSingleton<IPodcastService, PodcastService>();

		return builder.Build();
	}

    /// <summary>
    /// Sets application preferences.
    /// </summary>
	private static void LoadPreferences()
    {
        var config = BuildConfig();

        Preferences.Set("endpoint", config["endpoint"]);
        Preferences.Set("api_key", config["api_key"]);
        Preferences.Set("api_secret", config["api_secret"]);
        Preferences.Set("access_token", config["access_token"]);
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
