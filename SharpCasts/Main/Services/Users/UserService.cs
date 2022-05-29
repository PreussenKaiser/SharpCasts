using SharpCasts.Core.Contexts;
using SharpCasts.Main.Models;

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
    public User GetUserByCredentials(User user)
    {
        User foundUser = null;

        foreach (User u in this.database.Users)
        {
            if (u.Name == user.Name 
                && u.Password == user.Password)
            {
                foundUser = u;

                break;
            }
        }

        return foundUser;
    }

    /// <summary>
    /// Gets a user from the database by their unique identifier.
    /// </summary>
    /// <param name="id">The user to get.</param>
    /// <returns>The found user, null of none were found.</returns>
    public async Task<User> GetUser(int id)
        => await this.database.Users.FindAsync(id);

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
