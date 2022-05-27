using SharpCasts.Contexts;
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
    private readonly PodcastContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService">UserService</see> service.
    /// </summary>
    public UserService()
        => context = new PodcastContext();

    /// <summary>
    /// Adds a user to the database.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddUser(User user)
    {
    }

    /// <summary>
    /// Gets a user from the database.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The user, null if none were found.</returns>
    public Task<User> GetUser(User user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task RemoveUser(User user)
    {
    }
}
