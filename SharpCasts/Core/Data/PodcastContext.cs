using SharpCasts.Main.Models;

using Microsoft.EntityFrameworkCore;

namespace SharpCasts.Core.Contexts;

/// <summary>
/// The context which interacts with the SharpCasts Azure database.
/// </summary>
public class PodcastContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastContext"/> class.
    /// </summary>
    /// <param name="options"></param>
    public PodcastContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets subscribed podcasts in the database.
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; }
}
