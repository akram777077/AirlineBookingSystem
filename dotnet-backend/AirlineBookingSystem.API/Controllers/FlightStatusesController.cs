using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing flight status-related operations.
/// </summary>
[ApiController]
[Route("api/flight-statuses")]
public class FlightStatusesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Retrieves all available flight statuses.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="FlightStatusDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all flight statuses.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFlightStatuses()
    {
        var result = await sender.Send(new GetAllFlightStatusesQuery());
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific flight status by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight status.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="FlightStatusDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the flight status details.</response>
    /// <response code="404">If a flight status with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightStatusById(int id)
    {
        var query = new GetFlightStatusByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
