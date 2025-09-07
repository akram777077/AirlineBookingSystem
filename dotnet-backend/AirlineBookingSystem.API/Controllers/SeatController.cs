using AirlineBookingSystem.Application.Features.Seats.Commands.Create;
using AirlineBookingSystem.Application.Features.Seats.Commands.Update;
using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing seat-related operations.
/// </summary>
[ApiVersion("1.0")]
[Route(SeatRoutes.Base)]
[ApiController]
[EnableRateLimiting("fixed")]
public class SeatController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Creates a new seat record in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing details for the new seat.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created seat if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created seat.</response>
    /// <response code="400">If the provided seat data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSeat([FromBody] CreateSeatDto dto)
    {
        var result = await sender.Send(new CreateSeatCommand(dto));
        return result.IsSuccess ? CreatedAtAction(nameof(GetSeatById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Updates an existing seat record identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the seat to update.</param>
    /// <param name="dto">The data transfer object containing updated details for the seat.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the seat was updated successfully (no content).</response>
    /// <response code="404">If a seat with the specified ID is not found.</response>
    /// <response code="400">If the provided seat data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut(SeatRoutes.GetById)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSeat(int id, [FromBody] UpdateSeatDto dto)
    {
        var result = await sender.Send(new UpdateSeatCommand(id, dto));
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific seat by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the seat.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="SeatDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the seat details.</response>
    /// <response code="404">If a seat with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(SeatRoutes.GetById)]
    [ProducesResponseType(typeof(SeatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSeatById(int id)
    {
        var result = await sender.Send(new GetSeatByIdQuery(id));
        return this.ToActionResult(result);
    }
}