using SharpCasts.Contexts;

namespace SharpCasts.Main.Services;

/// <summary>
/// The service which gets subscribed podcasts from a remote Azure SQL Server database.
/// </summary>
public class SubscribedService : ISubscribedService
{
    /// <summary>
    /// The context which gets subscribed podcasts from the database.
    /// </summary>
    private readonly PodcastContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscribedService">SubscribedService</see> service.
    /// </summary>
    public SubscribedService()
        => this.context = new PodcastContext();
}
