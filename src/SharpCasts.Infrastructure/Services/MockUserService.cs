using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// The service that mimics the remote Azure MSSQL database.
/// </summary>
public class MockUserService : IUserService
{
    /// <summary>
    /// The users in the service.
    /// </summary>
    private readonly List<User> users;

    /// <summary>
    /// Initializes a new istance of the <see cref="MockUserService"/> class.
    /// </summary>
    public MockUserService()
        => this.users = new List<User>
        {
            new()
            {
                Id = 1,
                Name = "pkaiser",
                Password = "pepper"
            }
        };

    /// <summary>
    /// Adds a user to the data store.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task AddUser(User user)
    {
        this.users.Add(user);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets a user from the data store.
    /// </summary>
    /// <param name="id">The identifier of the user to get.</param>
    /// <returns>The found user, null if none were found.</returns>
    public async Task<User> GetUser(int id)
    {
        foreach (User user in this.users)
            if (user.Id == id)
                return await Task.FromResult(user);

        return null;
    }

    /// <summary>
    /// Gets a user in the data store by their credentials.
    /// </summary>
    /// <param name="user">The user to get.</param>
    /// <returns>The found user, null if none were found.</returns>
    public User GetUserByCredentials(User user)
    {
        foreach (User u in this.users)
            if (u.Name == user.Name
                && u.Password == user.Password)
            {
                return u;
            }

        return null;
    }

    /// <summary>
    /// Removes a user from the data store.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveUser(User user)
    {
        this.users.Remove(user);

        return Task.CompletedTask;
    }
}
