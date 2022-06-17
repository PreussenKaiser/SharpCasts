using Microsoft.EntityFrameworkCore;
using SharpCasts.Core.Models;

namespace SharpCasts.Infrastructure.Data;

/// <summary>
/// The context for querying user data.
/// </summary>
public class PodcastContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastContext"/> class.
    /// </summary>
    /// <param name="options">Options for the context.</param>
    public PodcastContext(DbContextOptions options)
        : base(options)
    {
        SQLitePCL.Batteries_V2.Init();

        this.Database.EnsureCreated();
    }

    /// <summary>
    /// Gets or sets subscriptions in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets subscriptions on the database.
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; }
}
