using AirlineBookingSystem.Application.Features.Airplanes.Commands.CreateAirplane;
using AirlineBookingSystem.Application.Features.Airplanes.Commands.UpdateAirplane;
using AirlineBookingSystem.Application.Features.Airplanes.Queries.SearchAirplanes;
using AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace AirlineBookingSystem.API.Controllers;

[Route("api/airplanes")]
[ApiController]
public class AirplaneController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAirplane([FromBody] CreateAirplaneDto dto)
    {
        var result = await sender.Send(new CreateAirplaneCommand(dto));
        return result.IsSuccess ? CreatedAtAction(nameof(GetAirplaneById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }

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

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<AirplaneDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAirplanes([FromQuery] AirplaneSearchFilter filter)
    {
        var result = await sender.Send(new SearchAirplanesQuery(filter));

        if (result.IsSuccess && result is PagedResult<List<AirplaneDto>> pagedResult)
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

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

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