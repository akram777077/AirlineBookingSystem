using AirlineBookingSystem.Application.Features.Terminals.Commands.CreateTerminal;
using AirlineBookingSystem.Application.Features.Terminals.Commands.UpdateTerminal;
using AirlineBookingSystem.Application.Features.Terminals.Queries.SearchTerminals;
using AirlineBookingSystem.Application.Features.Terminals.Queries.GetTerminalById;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results.Error;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/terminals")]
public class TerminalsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTerminal([FromBody] CreateTerminalCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

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

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<TerminalDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchTerminals([FromQuery] TerminalSearchFilter filter)
    {
        var query = new SearchTerminalsQuery(filter);
        var result = await sender.Send(query);

        if (result.IsSuccess && result is PagedResult<List<TerminalDto>> pagedResult)
        {
            var routeValues = new RouteValueDictionary(filter.ToDictionary().Select(x => new KeyValuePair<string, object>(x.Key, x.Value)));

            if (pagedResult.PageNumber < pagedResult.TotalPages)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber + 1;
                pagedResult.Metadata["nextPageUri"] = Url.Link(null, routeValues);
            }

            if (pagedResult.PageNumber > 1)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber - 1;
                pagedResult.Metadata["prevPageUri"] = Url.Link(null, routeValues);
            }
        }
        return this.ToActionResult(result);
    }

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
