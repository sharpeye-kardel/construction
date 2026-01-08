using System.ComponentModel.DataAnnotations.Schema;

namespace constprojectapi.Models
{
  /// <summary>
  /// Represents a user account entity in the database.
  /// Maps to the 'users' table in PostgreSQL.
  /// </summary>
  [Table("users")]
  public class User
  {
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    /// <remarks>
    /// Passwords are hashed using PBKDF2 with the user's salt before storage.
    /// Never stores plain text passwords.
    /// </remarks>
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the cryptographic salt used for password hashing.
    /// </summary>
    /// <remarks>
    /// Generated using RNGCryptoServiceProvider for secure random bytes.
    /// Used in combination with the password for PBKDF2 hashing.
    /// </remarks>
    [Column("salt")]
    public string Salt { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    /// <remarks>Must be unique across all users in the system.</remarks>
    [Column("email")]
    public string Email { get; set; }
  }
}