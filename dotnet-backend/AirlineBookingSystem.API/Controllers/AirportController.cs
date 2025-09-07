using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.Airports.Commands.Update;
using AirlineBookingSystem.Application.Features.Airports.Queries.GetById;
using AirlineBookingSystem.Application.Features.Airports.Queries.Search;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;
using AirlineBookingSystem.API.Routes.BaseRoute;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing airport-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(_airportRoutes.BaseRoute)]
[EnableRateLimiting("fixed")]
public class AirportController(ISender sender) : ControllerBase
{
    private readonly AirportRoutes _airportRoutes = new();

    /// <summary>
    /// Searches for airports based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters for airports, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{AirportSearchResultDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of airports matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<AirportSearchResultDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAirports([FromQuery] AirportSearchFilter filter)
    {
        var query = new SearchAirportsQuery(filter);
        var result = await sender.Send(query);

        if (result.IsSuccess && result is { } pagedResult)
        {
            var routeValues = new RouteValueDictionary(filter.ToDictionary().Select(x => new KeyValuePair<string, object?>(x.Key, x.Value)));

            if (pagedResult.PageNumber < pagedResult.TotalPages)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber + 1;
                pagedResult.Metadata["nextPageUri"] = Url.Link(null, routeValues)!;
            }

            if (pagedResult.PageNumber > 1)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber - 1;
                pagedResult.Metadata["prevPageUri"] = Url.Link(null, routeValues)!;
            }
        }

        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves detailed information about a specific airport by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the airport.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="AirportDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the airport details.</response>
    /// <response code="404">If an airport with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(_airportRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(AirportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAirportById(int id)
    {
        var query = new GetAirportByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Updates an existing airport record identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the airport to update.</param>
    /// <param name="airportDto">The data transfer object containing updated details for the airport.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the airport was updated successfully (no content).</response>
    /// <response code="404">If an airport with the specified ID is not found.</response>
    /// <response code="400">If the provided airport data is invalid or IDs do not match.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut(_airportRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAirport(int id, [FromBody] UpdateAirportDto airportDto)
    {
        if (id != airportDto.Id)
        {
            return BadRequest("Id in path and body do not match.");
        }
        var command = new UpdateAirportCommand(airportDto);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }
}