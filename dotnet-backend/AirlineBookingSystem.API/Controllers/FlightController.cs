using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AirlineBookingSystem.Application.Features.Flights.Commands.Create;
using AirlineBookingSystem.Application.Features.Flights.Commands.Delete;
using AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsArrived;
using AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsDeparted;
using AirlineBookingSystem.Application.Features.Flights.Commands.Update;
using AirlineBookingSystem.Application.Features.Flights.Queries.GetById;
using AirlineBookingSystem.Application.Features.Flights.Queries.Search;

using Microsoft.AspNetCore.RateLimiting;
using AirlineBookingSystem.API.Routes;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing flight-related operations.
/// </summary>
[ApiVersion("1.0")]
[Route(FlightRoutes.BaseRoute)]
[ApiController]
[Authorize]
[EnableRateLimiting("fixed")]
public class FlightController(ISender sender) : ControllerBase
{
    // ...existing code...
    /// <summary>
    /// Retrieves detailed information about a specific flight by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="FlightDetailsDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the flight details.</response>
    /// <response code="404">If a flight with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(FlightRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(FlightDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightById(int id)
    {
    var result = await sender.Send(new GetFlightByIdQuery(id));
    return result.ToActionResult();
    }

    /// <summary>
    /// Searches for flights based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters such as origin, destination, dates, and pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{FlightSearchResultDto}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of flights matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(FlightRoutes.Search)]
    [ProducesResponseType(typeof(PagedResult<FlightSearchResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchFlights([FromQuery] FlightSearchFilter filter)
    {
        var result = await sender.Send(new SearchFlightsQuery(filter));

        if (result.IsSuccess && result.Value is { } )
        {
            var routeValues = new RouteValueDictionary(filter.ToDictionary().Select(x => new KeyValuePair<string, object?>(x.Key, x.Value)));

            if (result.Value.PageNumber < result.Value.TotalPages)
            {
                routeValues["pageNumber"] = result.Value.PageNumber + 1;
                result.Value.Metadata["nextPageUri"] = Url.Link(null, routeValues)!;
            }

            if (result.Value.PageNumber > 1)
            {
                routeValues["pageNumber"] = result.Value.PageNumber - 1;
                result.Value.Metadata["prevPageUri"] = Url.Link(null, routeValues)!;
            }
        }

        return result.ToActionResult();
    }

    /// <summary>
    /// Creates a new flight in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing details for the new flight.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created flight if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created flight.</response>
    /// <response code="400">If the provided flight data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto dto)
    {
    var result = await sender.Send(new CreateFlightCommand(dto));
    return result.ToActionResult(nameof(GetFlightById), new { id = result.Value });
    }

    /// <summary>
    /// Updates an existing flight identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight to update.</param>
    /// <param name="dto">The data transfer object containing updated details for the flight.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the flight was updated successfully (no content).</response>
    /// <response code="404">If a flight with the specified ID is not found.</response>
    /// <response code="400">If the provided flight data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut(FlightRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFlight(int id, [FromBody] UpdateFlightDto dto)
    {
    var result = await sender.Send(new UpdateFlightCommand(id, dto));
    return result.ToActionResult();
    }

    /// <summary>
    /// Marks a specific flight as departed.
    /// </summary>
    /// <param name="id">The unique identifier of the flight to mark as departed.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the flight was marked as departed successfully (no content).</response>
    /// <response code="404">If a flight with the specified ID is not found.</response>
    /// <response code="400">If the flight cannot be marked as departed (e.g., invalid current status).</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPatch(FlightRoutes.MarkAsDeparted)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFlightAsDeparted(int id)
    {
    var result = await sender.Send(new MarkFlightAsDepartedCommand(id));
    return result.ToActionResult();
    }

    /// <summary>
    /// Marks a specific flight as arrived.
    /// </summary>
    /// <param name="id">The unique identifier of the flight to mark as arrived.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the flight was marked as arrived successfully (no content).</response>
    /// <response code="404">If a flight with the specified ID is not found.</response>
    /// <response code="400">If the flight cannot be marked as arrived (e.g., invalid current status).</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPatch(FlightRoutes.MarkAsArrived)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFlightAsArrived(int id)
    {
    var result = await sender.Send(new MarkFlightAsArrivedCommand(id));
    return result.ToActionResult();
    }

    /// <summary>
    /// Deletes a flight from the system by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the flight was deleted successfully (no content).</response>
    /// <response code="404">If a flight with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete(FlightRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFlight(int id)
    {
    var result = await sender.Send(new DeleteFlightCommand(id));
    return result.ToActionResult();
    }
}
