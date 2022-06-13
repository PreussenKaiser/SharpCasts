using SharpCasts.Core.Models;

namespace SharpCasts.Core.Services;

/// <summary>
/// The interface which implements subscribed podcasts services.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Creates a subscription in the service.
    /// </summary>
    /// <param name="subscription">The subscription to create.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task AddSubscriptionAsync(Subscription subscription);

    /// <summary>
    /// Gets subscriptions from the service that were made by a user.
    /// </summary>
    /// <param name="userId">The user to get subscriptions for.</param>
    /// <returns>A list of subscriptions made by the user.</returns>
    public Task<List<Subscription>> GetSubscriptionsForAsync(int userId);

    /// <summary>
    /// Gets a subscription by a user for a podcast.
    /// </summary>
    /// <param name="userId">The user to search for.</param>
    /// <param name="podcastId">The podcast which they may have subscribed to.</param>
    /// <returns>The found subscription, null if none were found.</returns>
    public Task<Subscription> GetSubscriptionAsync(int userId, int podcastId);

    /// <summary>
    /// Deletes a subscription from the service.
    /// </summary>
    /// <param name="subscription">The subscription to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task UnsubscribeAsync(Subscription subscription);
}
