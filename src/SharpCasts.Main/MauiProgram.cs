using SharpCasts.Main.Extensions.Builders;

using System.Reflection;

using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui;

namespace SharpCasts.Main;

/// <summary>
/// Initializes the application.
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

        var config = BuildConfig();
        builder.Configuration.AddConfiguration(config);

		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .LoadViews()
            .LoadViewmodels()
            .LoadServices(config)
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
				     .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
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
