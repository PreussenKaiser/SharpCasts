using SharpCasts.Infrastructure.Data;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// Queries podcast subscriptions using <see cref="PodcastContext"/>.
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

    /// <summary>
    /// Gets a subscription from the database.
    /// </summary>
    /// <param name="userId">The user who made the subscription.</param>
    /// <param name="podcastId">The podcast the user subscribed to.</param>
    /// <returns>The found subscription, null if none were found.</returns>
    public async Task<Subscription> GetSubscriptionAsync(int userId, int podcastId)
    {
        Subscription subscription = (Subscription)this.database.Subscriptions
                                                 .Where(s => s.UserId == userId
                                                             && s.PodcastId == podcastId);

        return await Task.FromResult(subscription);
    }

    /// <summary>
    /// Deletes a subscription from the database.
    /// </summary>
    /// <param name="subscription">The subscription to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task UnsubscribeAsync(Subscription subscription)
    {
        this.database.Subscriptions.Remove(subscription);
        await this.database.SaveChangesAsync();
    }
}
