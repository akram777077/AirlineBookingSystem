using AirlineBookingSystem.Application.Features.Flights.Query.ById;
using AirlineBookingSystem.Application.Features.Flights.Query.Search;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;
[Route("api/flights")]
[ApiController]
public class FlightController(ISender sender) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightById(int id)
    {
        var result = await sender.Send(new GetFlightByIdCommand(id));
        return this.ToActionResult(result);
    }
    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyList<FlightSearchResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchFlights([FromQuery] FlightSearchFilter filter)
    {
        var result = await sender.Send(new SearchFlightsQuery(filter));
        return this.ToActionResult(result);
    }
}