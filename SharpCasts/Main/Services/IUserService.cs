using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services;

/// <summary>
/// The interface which implements user query methods.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Adds a user to the service.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task AddUser(User user);

    /// <summary>
    /// Removes a user from the service.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveUser(User user);
}
