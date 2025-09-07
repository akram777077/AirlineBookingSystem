using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing class type-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/class-types")]
[EnableRateLimiting("fixed")]
public class ClassTypesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Retrieves all available class types.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="ClassTypeDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all class types.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClassTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllClassTypes()
    {
        var result = await sender.Send(new GetAllClassTypesQuery());
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific class type by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the class type.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="ClassTypeDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the class type details.</response>
    /// <response code="404">If a class type with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClassTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetClassTypeById(int id)
    {
        var query = new GetClassTypeByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
