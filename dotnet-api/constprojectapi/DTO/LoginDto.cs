using System.ComponentModel.DataAnnotations;

namespace constprojectapi.Dto
{
    /// <summary>
    /// Data transfer object for user login authentication.
    /// Contains credentials required for user authentication.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <remarks>Required field. Must be a valid email address format.</remarks>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <remarks>Required field. Password is verified against the stored hash.</remarks>
        [Required]
        public string Password { get; set; }
    }
}