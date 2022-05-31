using SharpCasts.Core.Contexts;
using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services.Subscriptions;

/// <summary>
/// The service which gets subscribed podcasts from a remote Azure SQL Server database.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    /// <summary>
    /// The context which gets subscribed podcasts from the database.
    /// </summary>
    private readonly PodcastContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionService"/> service.
    /// </summary>
    public SubscriptionService()
        => this.database = new PodcastContext();

    /// <summary>
    /// Adds a subscription to the database.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddSubscription(Subscription subscription)
    {
        await this.database.Subscriptions.AddAsync(subscription);
        await this.database.SaveChangesAsync();
    }
}
