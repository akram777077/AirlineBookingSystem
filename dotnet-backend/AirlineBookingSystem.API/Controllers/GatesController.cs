using AirlineBookingSystem.Application.Features.Gates.Commands.Create;
using AirlineBookingSystem.Application.Features.Gates.Commands.Update;
using AirlineBookingSystem.Application.Features.Gates.Queries.GetById;
using AirlineBookingSystem.Application.Features.Gates.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing gate-related operations.
/// </summary>
[ApiController]
[Route("api/gates")]
public class GatesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Creates a new gate record in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing details for the new gate.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created gate if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created gate.</response>
    /// <response code="400">If the provided gate data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGate([FromBody] CreateGateDto dto)
    {
        var result = await sender.Send(new CreateGateCommand(dto));
        return this.ToActionResult(result, nameof(GetGateById), new { id = result.Value });
    }

    /// <summary>
    /// Updates an existing gate record identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the gate to update.</param>
    /// <param name="dto">The data transfer object containing updated details for the gate.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the gate was updated successfully (no content).</response>
    /// <response code="404">If a gate with the specified ID is not found.</response>
    /// <response code="400">If the provided gate data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGate(int id, [FromBody] UpdateGateDto dto)
    {
        var result = await sender.Send(new UpdateGateCommand(id, dto));
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific gate by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the gate.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="GateDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the gate details.</response>
    /// <response code="404">If a gate with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGateById(int id)
    {
        var result = await sender.Send(new GetGateByIdQuery(id));
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Searches for gates based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters for gates, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{GateDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of gates matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<GateDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchGates([FromQuery] GateSearchFilter filter)
    {
        var result = await sender.Send(new SearchGatesQuery(filter));

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
}
