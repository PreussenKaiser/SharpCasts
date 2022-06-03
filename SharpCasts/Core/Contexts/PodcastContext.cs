using SharpCasts.Main.Models;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SharpCasts.Core.Contexts;

/// <summary>
/// The context which interacts with the SharpCasts Azure database.
/// </summary>
public class PodcastContext : DbContext
{
    /// <summary>
    /// Gets or sets users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets subscribed podcasts in the database.
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; }

    /// <summary>
    /// Connects the context to the remote database.
    /// </summary>
    /// <param name="optionsBuilder">Sets the connection string.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(BuildConnectionString());

    /// <summary>
    /// Saves changes to the Azure database.
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
        => base.SaveChanges();

    /// <summary>
    /// Saves changes asynchronously to the Azure database.
    /// </summary>
    /// <returns></returns>
    public async Task<int> SaveChangesAsync()
        => await base.SaveChangesAsync();

    /// <summary>
    /// Builds the connection string for the Azure SQL Server database.
    /// </summary>
    /// <returns>The connection string for the remote database.</returns>
    private static string BuildConnectionString()
    {
        string source = Preferences.Get("Source", "");
        string initialCatalog = Preferences.Get("InitialCatalog", "");
        string userId = Preferences.Get("UserID", "");
        string password = Preferences.Get("Password", "");

        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
        {
            DataSource = source,
            InitialCatalog = initialCatalog,
            PersistSecurityInfo = false,
            UserID = userId,
            Password = password,
            MultipleActiveResultSets = false,
            Encrypt = true,
            TrustServerCertificate = false,
            ConnectTimeout = 30
        };

        return connectionString.ToString();
    }
}
