using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services.Users;

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
    /// Gets a user from the service using their credentials.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The found user.</returns>
    public User GetUserByCredentials(User user);

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id">The user to get.</param>
    /// <returns>The found user.</returns>
    public Task<User> GetUser(int id);

    /// <summary>
    /// Removes a user from the service.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveUser(User user);
}
