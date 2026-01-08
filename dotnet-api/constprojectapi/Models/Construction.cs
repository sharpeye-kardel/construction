using System.ComponentModel.DataAnnotations.Schema;

namespace constprojectapi.Models
{
  /// <summary>
  /// Represents a construction project entity in the database.
  /// Maps to the 'constructions' table in PostgreSQL.
  /// </summary>
  [Table("constructions")]
  public class Construction
  {
    /// <summary>
    /// Gets or sets the unique identifier of the construction project.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the construction project.
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the physical location or address of the construction project.
    /// </summary>
    [Column("location")]
    public string Location { get; set; }

    /// <summary>
    /// Gets or sets the current stage of the construction project.
    /// </summary>
    /// <remarks>
    /// Stage values: 1 = Concept, 2 = Design &amp; Documentation, 3 = Pre-Construction, 4 = Construction.
    /// </remarks>
    [Column("stage")]
    public int Stage { get; set; }

    /// <summary>
    /// Gets or sets the category of the construction project.
    /// </summary>
    /// <remarks>Examples: Education, Health, Office, Other.</remarks>
    [Column("category")]
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the construction project.
    /// </summary>
    [Column("details")]
    public string Details { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created this construction project.
    /// </summary>
    [Column("creator_id")]
    public int CreatorId { get; set; }

    private DateTime _startDate;

    /// <summary>
    /// Gets or sets the start date of the construction project.
    /// </summary>
    /// <remarks>
    /// The setter automatically converts the value to UTC for consistent storage.
    /// </remarks>
    [Column("start_date")]
    public DateTime StartDate
    {
      get => _startDate;
      set => _startDate = value.ToUniversalTime(); // Ensure that value is stored in UTC
    }

    /// <summary>
    /// Gets the start date converted to the local time zone.
    /// </summary>
    /// <returns>The start date in local time.</returns>
    public DateTime GetStartDateInLocalTime()
    {
      return TimeZoneInfo.ConvertTimeFromUtc(_startDate, TimeZoneInfo.Local);
    }

    /// <summary>
    /// Sets the start date from a local time value, converting it to UTC for storage.
    /// </summary>
    /// <param name="localTime">The local time value to set. If null, uses current UTC time.</param>
    public void SetStartDateFromLocalTime(DateTime? localTime)
    {
      if (localTime.HasValue)
      {
        _startDate = TimeZoneInfo.ConvertTimeToUtc(localTime.Value);
      }
      else
      {
        _startDate = DateTime.UtcNow;
      }
    }
  }
}