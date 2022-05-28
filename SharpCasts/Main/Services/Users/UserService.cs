using SharpCasts.Contexts;
using SharpCasts.Main.Models;
using System.Linq;

namespace SharpCasts.Main.Services.Users;

/// <summary>
/// The service which gets users from an Azure database.
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// The context to connect to the database with.
    /// </summary>
    private readonly PodcastContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> service.
    /// </summary>
    public UserService()
        => this.database = new PodcastContext();

    /// <summary>
    /// Adds a user to the database.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddUser(User user)
    {
        await this.database.Users.AddAsync(user);

        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a user from the database.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The user, null if none were found.</returns>
    public async Task<User> GetUser(User user)
    {
        User foundUser = this.database.Users
                    .Select(u => u.Name == user.Name && u.Password == user.Password)
                    as User;

        return foundUser;
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task RemoveUser(User user)
    {
        this.database.Remove(user);

        await this.database.SaveChangesAsync();
    }
}
