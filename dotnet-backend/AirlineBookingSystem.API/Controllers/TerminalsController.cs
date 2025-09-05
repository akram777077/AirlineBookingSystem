using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.Terminals.Commands.Create;
using AirlineBookingSystem.Application.Features.Terminals.Commands.Update;
using AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;
using AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing terminal-related operations.
/// </summary>
[ApiController]
[Route("api/terminals")]
public class TerminalsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Creates a new terminal record in the system.
    /// </summary>
    /// <param name="command">The command containing data for the new terminal.</param>
    /// <returns>An <see cref="IActionResult"/> containing the ID of the newly created terminal if successful, or an error.</returns>
    /// <response code="201">Returns the ID of the newly created terminal.</response>
    /// <response code="400">If the provided terminal data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTerminal([FromBody] CreateTerminalCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Updates an existing terminal record.
    /// </summary>
    /// <param name="command">The command containing updated data for the terminal.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the terminal was updated successfully (no content).</response>
    /// <response code="404">If a terminal with the specified ID is not found.</response>
    /// <response code="400">If the provided terminal data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTerminal([FromBody] UpdateTerminalCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Searches for terminals based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters for terminals, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{TerminalDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of terminals matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<TerminalDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchTerminals([FromQuery] TerminalSearchFilter filter)
    {
        var query = new SearchTerminalsQuery(filter);
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
    /// Retrieves a specific terminal by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the terminal.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="TerminalDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the terminal details.</response>
    /// <response code="404">If a terminal with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TerminalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTerminalById(int id)
    {
        var query = new GetTerminalByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
