using constprojectapi.Models;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Data
{
  /// <summary>
  /// Database context for managing Construction entities.
  /// Provides data access operations for construction projects using Entity Framework Core.
  /// </summary>
  public class ConstructionContext : DbContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructionContext"/> class.
    /// </summary>
    /// <param name="options">The database context options for configuring the context.</param>
    public ConstructionContext(DbContextOptions<ConstructionContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for Construction entities.
    /// Provides access to query and save instances of <see cref="Construction"/>.
    /// </summary>
    public DbSet<Construction> Constructions { get; set; }
  }
}