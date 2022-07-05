using SharpCasts.Core.Models;

namespace SharpCasts.Core.Services;

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
    public Task AddUserAsync(User user);

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id">The user to get.</param>
    /// <returns>The found user.</returns>
    public Task<User> GetUserAsync(int id);

    /// <summary>
    /// Gets a user from the service using their credentials.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The found user.</returns>
    public User GetUserByCredentials(User user);

    /// <summary>
    /// Gets a user from the service by their username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The found user.</returns>
    public User GetUserByUsername(string username);

    /// <summary>
    /// Removes a user from the service.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveUserAsync(User user);
}
