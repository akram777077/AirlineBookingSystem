using AirlineBookingSystem.Application.Features.Flights.Command;
using AirlineBookingSystem.Application.Features.Flights.Command.Create;
using AirlineBookingSystem.Application.Features.Flights.Command.Delete;
using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsArrived;
using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsDeparted;
using AirlineBookingSystem.Application.Features.Flights.Command.Update;
using AirlineBookingSystem.Application.Features.Flights.Query.ById;
using AirlineBookingSystem.Application.Features.Flights.Query.Search;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;
[Route("api/flights")]
[ApiController]
public class FlightController(ISender sender) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightById(int id)
    {
        var result = await sender.Send(new GetFlightByIdQuery(id));
        return this.ToActionResult(result);
    }
    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyList<FlightSearchResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchFlights([FromQuery] FlightSearchFilter filter)
    {
        var result = await sender.Send(new SearchFlightsQuery(filter));
        return this.ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto dto)
    {
        var result = await sender.Send(new CreateFlightCommand(dto));
        return this.ToActionResult(result, nameof(GetFlightById), new { id = result.Value });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFlight(int id, [FromBody] UpdateFlightDto dto)
    {
        var result = await sender.Send(new UpdateFlightCommand(id, dto));
        return this.ToActionResult(result);
    }

    [HttpPatch("{id:int}/mark-departed")]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFlightAsDeparted(int id)
    {
        var result = await sender.Send(new MarkFlightAsDepartedCommand(id));
        return this.ToActionResult(result);
    }

    [HttpPatch("{id:int}/mark-arrived")]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFlightAsArrived(int id)
    {
        var result = await sender.Send(new MarkFlightAsArrivedCommand(id));
        return this.ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        var result = await sender.Send(new DeleteFlightCommand(id));
        return this.ToActionResult(result);
    }
}