using constprojectapi.Models;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Data
{
  /// <summary>
  /// Database context for managing User entities.
  /// Provides data access operations for user accounts using Entity Framework Core.
  /// </summary>
  public class UserContext : DbContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UserContext"/> class.
    /// </summary>
    /// <param name="options">The database context options for configuring the context.</param>
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for User entities.
    /// Provides access to query and save instances of <see cref="User"/>.
    /// </summary>
    public DbSet<User> Users { get; set; }
  }
}