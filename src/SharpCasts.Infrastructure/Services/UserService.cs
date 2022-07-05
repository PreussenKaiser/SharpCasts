using SharpCasts.Infrastructure.Data;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// Queries users from <see cref="PodcastContext"/>.
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// The database to get users with.
    /// </summary>
    private readonly PodcastContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="context">The database to get users with.</param>
    public UserService(PodcastContext context)
        => this.database = context;

    /// <summary>
    /// Adds a user to the database.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddUserAsync(User user)
    {
        // TODO: Hash passwords.
        await this.database.Users.AddAsync(user);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a user from the database by their unique identifier.
    /// </summary>
    /// <param name="id">The user to get.</param>
    /// <returns>The found user, null of none were found.</returns>
    public async Task<User> GetUserAsync(int id)
        => await this.database.Users.FindAsync(id);

    /// <summary>
    /// Gets a user from the database.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The user, null if none were found.</returns>
    public User GetUserByCredentials(User user)
    {
        User foundUser = null;

        foreach (User u in this.database.Users)
            if (u.Name == user.Name
                && u.Password == user.Password)
            {
                foundUser = u;

                break;
            }

        return foundUser;
    }

    /// <summary>
    /// Gets a user from the database by their username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The found user.</returns>
    public User GetUserByUsername(string username)
    {
        foreach (User user in this.database.Users)
            if (user.Name == username)
                return user;

        return null;
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task RemoveUserAsync(User user)
    {
        this.database.Remove(user);

        await this.database.SaveChangesAsync();
    }
}
