using System.ComponentModel.DataAnnotations;

namespace constprojectapi.Dto
{
    /// <summary>
    /// Data transfer object for creating new user accounts.
    /// Contains credentials required for user registration.
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <remarks>Required field. Must be a valid email address format and unique across all users.</remarks>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <remarks>Required field with a minimum length of 6 characters. Will be hashed before storage.</remarks>
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
