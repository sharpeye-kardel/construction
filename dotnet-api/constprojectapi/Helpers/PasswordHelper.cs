using System;
using System.Security.Cryptography;
using System.Text;

namespace constprojectapi.Helpers
{
    /// <summary>
    /// Provides utility methods for secure password handling.
    /// Implements PBKDF2 hashing with cryptographically secure salt generation.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        /// <param name="size">The size of the salt in bytes. Default is 5.</param>
        /// <returns>A Base64-encoded salt string truncated to the specified size.</returns>
        public static string GenerateSalt(int size = 5)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer).Substring(0, size);
        }

        /// <summary>
        /// Hashes a password using PBKDF2 with the provided salt.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <param name="salt">The salt to use for hashing.</param>
        /// <returns>A Base64-encoded hash of the password.</returns>
        /// <remarks>Uses 10,000 iterations and produces a 32-byte hash.</remarks>
        public static string HashPassword(string password, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        /// <summary>
        /// Verifies a password against a stored hash and salt.
        /// </summary>
        /// <param name="enteredPassword">The plain text password to verify.</param>
        /// <param name="storedHash">The stored hash to compare against.</param>
        /// <param name="storedSalt">The salt used when creating the stored hash.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}
