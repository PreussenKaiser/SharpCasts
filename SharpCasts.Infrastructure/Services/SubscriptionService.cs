using SharpCasts.Infrastructure.Data;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// The service which gets subscribed podcasts from a remote Azure SQL Server database.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    /// <summary>
    /// The database to get podcast subscriptions with.
    /// </summary>
    private readonly PodcastContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionService"/> class.
    /// </summary>
    /// <param name="context">The database to get podcast subscriptions from.</param>
    public SubscriptionService(PodcastContext context)
        => this.database = context;

    /// <summary>
    /// Adds a subscription to the database.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await this.database.Subscriptions.AddAsync(subscription);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Subscription>> GetSubscriptionsForAsync(int userId)
    {
        List<Subscription> foundSubs = new();

        this.database.Subscriptions.ToList()
                                   .ForEach(s =>
        {
            if (s.UserId == userId)
                foundSubs.Add(s);
        });

        return await Task.FromResult(foundSubs);
    }
}
