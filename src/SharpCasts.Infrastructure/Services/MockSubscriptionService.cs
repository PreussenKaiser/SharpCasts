using SharpCasts.Infrastructure.Data;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// Mimics subscriptions queries from <see cref="PodcastContext"/>.
/// </summary>
public class MockSubscriptionService : ISubscriptionService
{
    /// <summary>
    /// The subscriptions in the service.
    /// </summary>
    private readonly List<Subscription> subscriptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockSubscriptionService"/> class.
    /// </summary>
    public MockSubscriptionService()
        => this.subscriptions = new List<Subscription>();

    /// <summary>
    /// Adds a subscription to the data store.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    /// <returns>Whether the tas was completed or not.</returns>
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await Task.Run(() =>
        {
            this.subscriptions.Add(subscription);
        });
    }

    /// <summary>
    /// Gets subscriptions for a user.
    /// </summary>
    /// <param name="userId">The user to get subscriptions for.</param>
    /// <returns>A list of subscriptions made by the user.</returns>
    public async Task<List<Subscription>> GetSubscriptionsForAsync(int userId)
    {
        List<Subscription> foundSubs = new();

        this.subscriptions.ForEach(s =>
        {
            if (s.UserId == userId)
                foundSubs.Add(s);
        });

        return await Task.FromResult(foundSubs);
    }

    /// <summary>
    /// Gets a subscription by a user for a podcast.
    /// </summary>
    /// <param name="userId">The user to search for.</param>
    /// <param name="podcastId">The podcast which they may have subscribed to.</param>
    /// <returns>The found subscription, null if none were found.</returns>
    public async Task<Subscription> GetSubscriptionAsync(int userId, int podcastId)
    {
        Subscription subscription = null;

        await Task.Run(() =>
        {
            subscription = this.subscriptions.Find(s
                => s.UserId == userId && s.PodcastId == podcastId);
        });

        return subscription;
    }

    /// <summary>
    /// Deletes a subscription from the mock data store.
    /// </summary>
    /// <param name="subscription">The subscription to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task UnsubscribeAsync(Subscription subscription)
    {
        await Task.Run(() => this.subscriptions.Remove(subscription));
    }
}
