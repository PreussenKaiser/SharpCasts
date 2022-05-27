using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharpCasts.Main.Models;

namespace SharpCasts.Contexts;

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
    public DbSet<Subscribed> Subscribed { get; set; }

    /// <summary>
    /// Connects the context to the remote database.
    /// </summary>
    /// <param name="optionsBuilder">Sets the connection string.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(this.BuildConnectionString());

    /// <summary>
    /// Builds the connection string for the Azure SQL Server database.
    /// </summary>
    /// <returns>The connection string for the remote database.</returns>
    private string BuildConnectionString()
    {
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
        {
            DataSource = "klukan.database.windows.net",
            InitialCatalog = "sharpcasts",
            PersistSecurityInfo = false,
            UserID = "pkaiser",
            Password = "&lJ@F<qANO[!(^BQc_HX",
            MultipleActiveResultSets = false,
            Encrypt = true,
            TrustServerCertificate = false,
            ConnectTimeout = 30
        };

        return connectionString.ToString();
    }
}
