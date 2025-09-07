using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;
using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;
using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExistingFlightClassDto = AirlineBookingSystem.Shared.DTOs.flightClasses.FlightClassDto;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing flight class-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(FlightClassRoutes.Base)]
[EnableRateLimiting("fixed")]
public class FlightClassesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Creates a new flight class in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing details for the new flight class.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created flight class if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created flight class.</response>
    /// <response code="400">If the provided flight class data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFlightClass([FromQuery] CreateFlightClassDto dto)
    {
        var result = await sender.Send(new CreateFlightClassCommand(dto));
        return this.ToActionResult(result,nameof(GetFlightClassById),new { id = result.Value });
    }

    /// <summary>
    /// Updates an existing flight class identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight class to update.</param>
    /// <param name="dto">The data transfer object containing updated details for the flight class.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="200">If the flight class was updated successfully.</response>
    /// <response code="400">If the provided flight class data is invalid or IDs do not match.</response>
    /// <response code="404">If a flight class with the specified ID is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut(FlightClassRoutes.GetById)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFlightClass(int id, [FromBody] UpdateFlightClassDto dto)
    {
        if (id != dto.Id) return this.ToActionResult(Result<int>.Failure("Id in the route must match the id in the body.", ResultStatusCode.BadRequest));
        var result = await sender.Send(new UpdateFlightClassCommand(dto));
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific flight class by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight class.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="ExistingFlightClassDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the flight class details.</response>
    /// <response code="404">If a flight class with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(FlightClassRoutes.GetById)]
    [ProducesResponseType(typeof(ExistingFlightClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightClassById(int id)
    {
        var query = new GetFlightClassByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves all flight classes associated with a specific flight ID.
    /// </summary>
    /// <param name="flightId">The unique identifier of the flight.</param>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="ExistingFlightClassDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of flight classes for the specified flight.</response>
    /// <response code="404">If no flight classes are found for the specified flight ID.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(FlightClassRoutes.GetByFlightId)]
    [ProducesResponseType(typeof(IEnumerable<ExistingFlightClassDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightClassesByFlightId(int flightId)
    {
        var query = new GetFlightClassesByFlightIdQuery(flightId);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
