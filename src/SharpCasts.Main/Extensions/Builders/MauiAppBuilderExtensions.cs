using SharpCasts.Main.ViewModels;
using SharpCasts.Main.Views;
using SharpCasts.Main.Helpers;

using SharpCasts.Audio;
using SharpCasts.Infrastructure.Data;
using SharpCasts.Infrastructure.Services;
using SharpCasts.Core.Services;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SharpCasts.Main.Extensions.Builders;

/// <summary>
/// Contains <see cref="MauiAppBuilder"/> extension methods.
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
        builder.Services.AddTransient<SubscriptionsPage>()
                        .AddTransient<DiscoverPage>()
                        .AddTransient<PodcastPage>()
                        .AddTransient<LoginPage>()
                        .AddTransient<RegisterPage>()
                        .AddTransient<ProfilePage>()
                        .AddTransient<SettingsPage>();

        return builder;
    }

    /// <summary>
    /// Loads viewmodels into the app builder.
    /// </summary>
    /// <param name="builder">The app builder to load views into.</param>
    /// <returns>The builder with initialized viewmodels.</returns>
    public static MauiAppBuilder LoadViewmodels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<SubscriptionsPageViewModel>()
                        .AddSingleton<DiscoverPageViewModel>()
                        .AddTransient<PodcastPageViewModel>()
                        .AddSingleton<LoginPageViewModel>()
                        .AddSingleton<RegisterPageViewModel>()
                        .AddSingleton<ProfilePageViewModel>()
                        .AddSingleton<SettingsPageViewModel>();

        return builder;
    }

    /// <summary>
    /// Loads services into the app builder.
    /// </summary>
    /// <param name="builder">The app builder to load services into.</param>
    /// <param name="config">Configuration for services.</param>
    /// <returns>The builder with initialized services.</returns>
    public static MauiAppBuilder LoadServices(this MauiAppBuilder builder, IConfiguration config)
    {
        builder.Services.AddSingleton<IPlayerService, PlayerService>()
#if ANDROID
                        .AddSingleton<INativeAudioService, Audio.Platforms.Android.NativeAudioService>()
#else
                        .AddSingleton<INativeAudioService, Audio.Platforms.Windows.NativeAudioService>()
#endif
                        .AddSingleton<IPodcastService, PodcastService>()
                        .AddSingleton<IUserService, UserService>()
                        .AddSingleton<ISubscriptionService, SubscriptionService>();

        Action<DbContextOptionsBuilder> options = null;
        if (Settings.UseLocal)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "SharpCasts.db3");
            options = o => o.UseSqlite($"Filename={dbPath}");
        }
        else
        {
            options = o => o.UseSqlServer(BuildConnectionString(config));
        }

        builder.Services.AddDbContext<PodcastContext>(options);

        return builder;
    }

    /// <summary>
    /// Builds the connection string for the database context.
    /// </summary>
    /// <returns>The connection string to the remote Azure MSSQL database.</returns>
    private static string BuildConnectionString(IConfiguration config)
    {
        string source = config["Database:Source"];
        string initialCatalog = config["Database:InitialCatalog"];
        string userId = config["Database:UserID"];
        string password = config["Database:Password"];

        SqlConnectionStringBuilder connectionString = new()
        {
            DataSource = source,
            InitialCatalog = initialCatalog,
            PersistSecurityInfo = false,
            UserID = userId,
            Password = password,
            MultipleActiveResultSets = false,
            Encrypt = true,
            TrustServerCertificate = false,
            ConnectTimeout = 30
        };

        return connectionString.ToString();
    }
}
