namespace constprojectapi.Response;

/// <summary>
/// Standard error response model for API error messages.
/// Used to return consistent error information to API clients.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the error message describing what went wrong.
    /// </summary>
    public string Message { get; set; }
}