using AirlineBookingSystem.Application.Features.Gates.Commands.Create;
using AirlineBookingSystem.Application.Features.Gates.Commands.Update;
using AirlineBookingSystem.Application.Features.Gates.Queries.GetById;
using AirlineBookingSystem.Application.Features.Gates.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/gates")]
public class GatesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGate([FromBody] CreateGateDto dto)
    {
        var result = await sender.Send(new CreateGateCommand(dto));
        return this.ToActionResult(result, nameof(GetGateById), new { id = result.Value });
    }

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

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<GateDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchGates([FromQuery] GateSearchFilter filter)
    {
        var result = await sender.Send(new SearchGatesQuery(filter));

        if (result.IsSuccess && result is PagedResult<List<GateDto>> pagedResult)
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
}
