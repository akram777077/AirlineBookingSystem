using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using System.Collections.Generic;
using AirlineBookingSystem.Application.Features.Airports.Commands.Update;
using AirlineBookingSystem.Application.Features.Airports.Queries.GetById;
using AirlineBookingSystem.Application.Features.Airports.Queries.Search;
using Microsoft.AspNetCore.Routing;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/airports")]
public class AirportController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<AirportSearchResultDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAirports([FromQuery] AirportSearchFilter filter)
    {
        var query = new SearchAirportsQuery(filter);
        var result = await sender.Send(query);

        if (result.IsSuccess && result is PagedResult<List<AirportSearchResultDto>> pagedResult)
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

    [HttpPut("{id:int}")]
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