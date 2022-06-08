using SharpCasts.Main.ViewModels;
using SharpCasts.Main.Views;

using SharpCasts.Audio;
using SharpCasts.Infrastructure.Data;
using SharpCasts.Infrastructure.Services;
using SharpCasts.Core.Services;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SharpCasts.Main.Extensions.Builders;

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
        builder.Services.AddSingleton<MainPageViewModel>()
                        .AddSingleton<DiscoverPageViewModel>()
                        .AddTransient<PodcastPageViewModel>()
                        .AddSingleton<LoginPageViewModel>()
                        .AddSingleton<RegisterPageViewModel>()
                        .AddSingleton<ProfilePageViewModel>();

        return builder;
    }

    /// <summary>
    /// Loads services into the app builder.
    /// </summary>
    /// <param name="builder">The app builder to load services into.</param>
    /// <returns>The builder with initialized services.</returns>
    public static MauiAppBuilder LoadServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IPlayerService, PlayerService>()
#if ANDROID
                        .AddSingleton<INativeAudioService, SharpCasts.Audio.Platforms.Android.NativeAudioService>()
#elif WINDOWS
                        .AddSingleton<INativeAudioService, SharpCasts.Audio.Platforms.Windows.NativeAudioService>()
#endif
                        .AddDbContext<PodcastContext>(options => options.UseSqlServer(BuildConnectionString()))
#if DEBUG
                        .AddSingleton<IPodcastService, MockPodcastService>()
                        .AddSingleton<IUserService, MockUserService>()
                        .AddSingleton<ISubscriptionService, MockSubscriptionService>();
#else
                        .AddSingleton<IPodcastService, PodcastService>()
                        .AddSingleton<IUserService, UserService>()
                        .AddSingleton<ISubscriptionService, SubscriptionService>();
#endif

        return builder;
    }

    /// <summary>
    /// Builds the connection string for the database context.
    /// </summary>
    /// <returns>The connection string to the remote Azure MSSQL database.</returns>
    private static string BuildConnectionString()
    {
        string source = Preferences.Get("Source", "");
        string initialCatalog = Preferences.Get("InitialCatalog", "");
        string userId = Preferences.Get("UserID", "");
        string password = Preferences.Get("Password", "");

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
