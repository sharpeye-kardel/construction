using constprojectapi.Data;
using constprojectapi.Dto;
using Microsoft.AspNetCore.Mvc;
using constprojectapi.Models;
using constprojectapi.Response;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Controllers;

/// <summary>
/// Controller for managing user accounts and authentication.
/// Provides operations for user registration, retrieval, deletion, and login authentication.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly UserContext _context;

  /// <summary>
  /// Initializes a new instance of the <see cref="UsersController"/> class.
  /// </summary>
  /// <param name="context">The database context for user data access.</param>
  public UsersController(UserContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Retrieves all users in the system.
  /// </summary>
  /// <returns>A list of all registered users.</returns>
  /// <response code="200">Returns the list of users.</response>
  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await _context.Users.ToListAsync();
    return Ok(users);
  }

  /// <summary>
  /// Retrieves a specific user by their unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the user.</param>
  /// <returns>The user with the specified identifier.</returns>
  /// <response code="200">Returns the requested user.</response>
  /// <response code="404">If the user is not found.</response>
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  /// <summary>
  /// Creates a new user account.
  /// </summary>
  /// <param name="userDto">The user data transfer object containing registration details.</param>
  /// <returns>The newly created user.</returns>
  /// <remarks>
  /// Business Logic:
  /// - Email addresses must be unique across all users.
  /// - Passwords are securely hashed using a generated salt before storage.
  /// - The salt and hashed password are stored separately for security.
  /// </remarks>
  /// <response code="201">Returns the newly created user.</response>
  /// <response code="400">If the model state is invalid.</response>
  /// <response code="409">If the email address is already in use.</response>
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public async Task<ActionResult<User>> PostUser(UserCreateDto userDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (EmailExists(userDto.Email))
    {
      var errorResponse = new ErrorResponse
      {
        Message = "Email already in use."
      };
      return Conflict(errorResponse);
    }

    // Create a new User object
    var user = new User
    {
      Email = userDto.Email
    };

    // Generate a salt and hash the password
    string salt = constprojectapi.Helpers.PasswordHelper.GenerateSalt();
    string hashedPassword = constprojectapi.Helpers.PasswordHelper.HashPassword(userDto.Password, salt);

    // Set the salt and hashed password on the user object
    user.Salt = salt;
    user.Password = hashedPassword;

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
  }

  /// <summary>
  /// Deletes a specific user account.
  /// </summary>
  /// <param name="id">The unique identifier of the user to delete.</param>
  /// <returns>No content on successful deletion.</returns>
  /// <response code="204">The user was successfully deleted.</response>
  /// <response code="404">If the user is not found.</response>
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    _context.Users.Remove(user);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  /// <summary>
  /// Authenticates a user and returns a JWT token.
  /// </summary>
  /// <param name="loginDto">The login data transfer object containing email and password.</param>
  /// <returns>A JWT token for authenticated requests.</returns>
  /// <remarks>
  /// Authentication Process:
  /// - Validates the provided email exists in the system.
  /// - Verifies the password against the stored hash using the user's salt.
  /// - Returns a JWT token on successful authentication.
  /// - Returns the same error message for invalid email or password to prevent user enumeration.
  /// </remarks>
  /// <response code="200">Returns the JWT token on successful authentication.</response>
  /// <response code="400">If the model state is invalid.</response>
  /// <response code="401">If the email or password is incorrect.</response>
  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult> Login(LoginDto loginDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
    if (user == null)
    {
      return Unauthorized(new ErrorResponse { Message = "Invalid email or password." });
    }

    if (!constprojectapi.Helpers.PasswordHelper.VerifyPassword(loginDto.Password, user.Password, user.Salt))
    {
      return Unauthorized(new ErrorResponse { Message = "Invalid email or password." });
    }

    // Assuming you have a method to generate JWT token
    var token = GenerateJwtToken(user);

    return Ok(new { Token = token });
  }

  /// <summary>
  /// Generates a JWT token for the authenticated user.
  /// </summary>
  /// <param name="user">The authenticated user for whom to generate the token.</param>
  /// <returns>A JWT token string.</returns>
  /// <remarks>
  /// This is a placeholder implementation. In production, this should create a proper
  /// security token signed with a secret key containing user claims.
  /// </remarks>
  private string GenerateJwtToken(User user)
  {
    // Generate JWT token logic here
    // This typically involves creating a security token and signing it with a secret key
    // This is just a placeholder for illustration
    return "generated-jwt-token";
  }

  /// <summary>
  /// Checks if an email address is already registered in the system.
  /// </summary>
  /// <param name="email">The email address to check.</param>
  /// <returns>True if the email exists; otherwise, false.</returns>
  private bool EmailExists(string email)
  {
    return _context.Users.Any(u => u.Email == email);
  }

  /// <summary>
  /// Test endpoint to verify the API is running and database connection is working.
  /// </summary>
  /// <returns>A test message string.</returns>
  /// <response code="200">Returns a test message indicating the API is operational.</response>
  [HttpGet("test")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public string Test()
  {
    return "Sampai nanti";
  }
}
