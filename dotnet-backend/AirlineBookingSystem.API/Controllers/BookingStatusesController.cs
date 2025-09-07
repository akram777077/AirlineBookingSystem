using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing booking status-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(BookingStatusRoutes.Base)]
[EnableRateLimiting("fixed")]
public class BookingStatusesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Retrieves all available booking statuses.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="BookingStatusDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all booking statuses.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBookingStatuses()
    {
        var result = await sender.Send(new GetAllBookingStatusesQuery());
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific booking status by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the booking status.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="BookingStatusDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the booking status details.</response>
    /// <response code="404">If a booking status with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(BookingStatusRoutes.GetById)]
    [ProducesResponseType(typeof(BookingStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBookingStatusById(int id)
    {
        var query = new GetBookingStatusByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
