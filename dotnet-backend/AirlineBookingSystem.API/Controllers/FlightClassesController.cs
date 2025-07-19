using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;
using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExistingFlightClassDto = AirlineBookingSystem.Shared.DTOs.flightClasses.FlightClassDto;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/flight-classes")]
public class FlightClassesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFlightClass([FromQuery] CreateFlightClassDto dto)
    {
        var result = await sender.Send(new CreateFlightClassCommand(dto));
        return this.ToActionResult(result,nameof(GetFlightClassById),new { id = result.Value });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ExistingFlightClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightClassById(int id)
    {
        var query = new GetFlightClassByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    [HttpGet("by-flight/{flightId:int}")]
    [ProducesResponseType(typeof(IEnumerable<ExistingFlightClassDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightClassesByFlightId(int flightId)
    {
        var query = new GetFlightClassesByFlightIdQuery(flightId);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
