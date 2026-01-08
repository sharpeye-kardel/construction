using constprojectapi.Data;
using constprojectapi.Dto;
using Microsoft.AspNetCore.Mvc;
using constprojectapi.Models;
using constprojectapi.Response;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Controllers;

/// <summary>
/// Controller for managing construction project resources.
/// Provides CRUD operations for construction projects including creation, retrieval, update, and deletion.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ConstructionsController : ControllerBase
{
  private readonly ConstructionContext _context;

  /// <summary>
  /// Initializes a new instance of the <see cref="ConstructionsController"/> class.
  /// </summary>
  /// <param name="context">The database context for construction data access.</param>
  public ConstructionsController(ConstructionContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Retrieves all construction projects.
  /// </summary>
  /// <returns>A list of all construction projects in the system.</returns>
  /// <response code="200">Returns the list of construction projects.</response>
  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<Construction>>> GetConstructions()
  {
    return await _context.Constructions.ToListAsync();
  }

  /// <summary>
  /// Retrieves a specific construction project by its unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the construction project.</param>
  /// <returns>The construction project with the specified identifier.</returns>
  /// <response code="200">Returns the requested construction project.</response>
  /// <response code="404">If the construction project is not found.</response>
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<Construction>> GetConstruction(int id)
  {
    var construction = await _context.Constructions.FindAsync(id);

    if (construction == null)
    {
      return NotFound();
    }

    return construction;
  }

  /// <summary>
  /// Creates a new construction project.
  /// </summary>
  /// <param name="constructionDto">The construction project data transfer object containing the project details.</param>
  /// <returns>The newly created construction project.</returns>
  /// <remarks>
  /// Business Logic:
  /// - For projects with stage less than 4 (non-Construction stage), the start date must be a future date.
  /// - The CreatorId is currently hardcoded to 1 and will be replaced with JWT data in future implementations.
  /// - The start date is converted from local time to UTC for storage.
  /// </remarks>
  /// <response code="201">Returns the newly created construction project.</response>
  /// <response code="400">If the model state is invalid or business validation fails.</response>
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Construction>> PostConstruction(ConstructionCreateDto constructionDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (!ValidateConstructionData(constructionDto, out var errorResponse))
    {
      return BadRequest(errorResponse);
    }

    var construction = new Construction
    {
      Name = constructionDto.Name,
      Stage = constructionDto.Stage,
      Category = constructionDto.Category,
      Location = constructionDto.Location,
      Details = constructionDto.Details,
      CreatorId = 1, //later, replace with data from JWT
    };
    construction.SetStartDateFromLocalTime(constructionDto.StartDate);

    _context.Constructions.Add(construction);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetConstruction), new { id = construction.Id }, construction);
  }

  /// <summary>
  /// Updates an existing construction project.
  /// </summary>
  /// <param name="id">The unique identifier of the construction project to update.</param>
  /// <param name="constructionDto">The construction project data transfer object containing the updated project details.</param>
  /// <returns>The updated construction project.</returns>
  /// <remarks>
  /// Business Logic:
  /// - For projects with stage less than 4 (non-Construction stage), the start date must be a future date.
  /// - The CreatorId is currently hardcoded to 1 and will be replaced with JWT data in future implementations.
  /// - The start date is converted from local time to UTC for storage.
  /// </remarks>
  /// <response code="200">Returns the updated construction project.</response>
  /// <response code="400">If the business validation fails.</response>
  /// <response code="404">If the construction project is not found.</response>
  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutConstruction(int id, ConstructionCreateDto constructionDto)
  {
    var construction = await _context.Constructions.FindAsync(id);
    if (construction == null)
    {
      return NotFound();
    }

    if (!ValidateConstructionData(constructionDto, out var errorResponse))
    {
      return BadRequest(errorResponse);
    }

    construction.Name = constructionDto.Name;
    construction.Stage = constructionDto.Stage;
    construction.Category = constructionDto.Category;
    construction.Location = constructionDto.Location;
    construction.Details = constructionDto.Details;
    construction.CreatorId = 1; //later, replace with data from JWT
    construction.SetStartDateFromLocalTime(constructionDto.StartDate);

    _context.Entry(construction).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return Ok(construction);
  }

  /// <summary>
  /// Deletes a specific construction project.
  /// </summary>
  /// <param name="id">The unique identifier of the construction project to delete.</param>
  /// <returns>No content on successful deletion.</returns>
  /// <response code="204">The construction project was successfully deleted.</response>
  /// <response code="404">If the construction project is not found.</response>
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteConstruction(int id)
  {
    var construction = await _context.Constructions.FindAsync(id);

    if (construction == null)
    {
      return NotFound();
    }

    _context.Constructions.Remove(construction);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  /// <summary>
  /// Validates the construction data according to business rules.
  /// </summary>
  /// <param name="constructionDto">The construction data transfer object to validate.</param>
  /// <param name="errorResponse">The error response object populated if validation fails.</param>
  /// <returns>True if validation passes; otherwise, false.</returns>
  /// <remarks>
  /// Validation Rules:
  /// - For projects with stage less than 4 (non-Construction stage), the start date must be a future date.
  /// - If no start date is provided, the current UTC time is used for validation.
  /// </remarks>
  private bool ValidateConstructionData(ConstructionCreateDto constructionDto, out ErrorResponse errorResponse)
  {
    errorResponse = null;

    var _startDate = new DateTime { };

    if (constructionDto.StartDate.HasValue)
    {
      _startDate = TimeZoneInfo.ConvertTimeToUtc(constructionDto.StartDate.Value);
    }
    else
    {
      _startDate = DateTime.UtcNow;
    }

    if (constructionDto.Stage < 4 && _startDate <= DateTime.UtcNow)
    {
      errorResponse = new ErrorResponse
      {
        Message = "StartDate must be a future date for non Construction stage project."
      };
      return false;
    }

    return true;
  }

}
