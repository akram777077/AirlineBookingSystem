using AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;
using AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;
using AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing airplane-related operations.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/airplanes")]
[ApiController]
[EnableRateLimiting("fixed")]
public class AirplaneController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Creates a new airplane record in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing details for the new airplane.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created airplane if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created airplane.</response>
    /// <response code="400">If the provided airplane data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAirplane([FromBody] CreateAirplaneDto dto)
    {
        var result = await sender.Send(new CreateAirplaneCommand(dto));
        return result.IsSuccess ? CreatedAtAction(nameof(GetAirplaneById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Updates an existing airplane record identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the airplane to update.</param>
    /// <param name="dto">The data transfer object containing updated details for the airplane.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the airplane was updated successfully (no content).</response>
    /// <response code="404">If an airplane with the specified ID is not found.</response>
    /// <response code="400">If the provided airplane data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAirplane(int id, [FromBody] UpdateAirplaneDto dto)
    {
        var result = await sender.Send(new UpdateAirplaneCommand(id, dto));
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }

    /// <summary>
    /// Searches for airplanes based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters for airplanes, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{AirplaneDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of airplanes matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<AirplaneDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAirplanes([FromQuery] AirplaneSearchFilter filter)
    {
        var result = await sender.Send(new SearchAirplanesQuery(filter));

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

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves detailed information about a specific airplane by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the airplane.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="AirplaneDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the airplane details.</response>
    /// <response code="404">If an airplane with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AirplaneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAirplaneById(int id)
    {
        var result = await sender.Send(new GetAirplaneByIdQuery(id));
        return this.ToActionResult(result);
    }
}