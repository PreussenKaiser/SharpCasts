using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// The service that mimics the remote Azure MSSQL database.
/// </summary>
public class MockSubscriptionService : ISubscriptionService
{
    /// <summary>
    /// The subscriptions in the service.
    /// </summary>
    private readonly List<Subscription> subscriptions;

    /// <summary>
    /// Initializes a new instanceof the <see cref="MockSubscriptionService"/> class.
    /// </summary>
    public MockSubscriptionService()
        => this.subscriptions = new List<Subscription>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                PodcastId = 1
            }
        };

    /// <summary>
    /// Adds a subscription to the data store.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    /// <returns>Whether the tas was completed or not.</returns>
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        subscription = await Task.FromResult(subscription);

        this.subscriptions.Add(subscription);
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
}
