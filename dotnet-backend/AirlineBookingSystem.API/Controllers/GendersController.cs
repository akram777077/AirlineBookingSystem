using AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.Genders.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing gender-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(GenderRoutes.BaseRoute)]
[EnableRateLimiting("fixed")]
public class GendersController(ISender sender) : ControllerBase
{
    // ...existing code...
    /// <summary>
    /// Retrieves all available genders.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="GenderDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all genders.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GenderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllGenders()
    {
        var result = await sender.Send(new GetAllGendersQuery());
    return result.ToActionResult();
    }

    /// <summary>
    /// Retrieves a specific gender by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the gender.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="GenderDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the gender details.</response>
    /// <response code="404">If a gender with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(GenderRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(GenderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGenderById(int id)
    {
        var query = new GetGenderByIdQuery(id);
        var result = await sender.Send(query);
    return result.ToActionResult();
    }
}
