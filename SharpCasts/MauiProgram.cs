using SharpCasts.Core.Extensions.Builders;

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
            .LoadViews()
            .LoadViewmodels()
            .LoadServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
				     .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

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
        Preferences.Set("InitialCatalog", config["Database:InitialCatalog"]);
        Preferences.Set("UserID", config["Database:UserID"]);
        Preferences.Set("Password", config["Database:Password"]);
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
