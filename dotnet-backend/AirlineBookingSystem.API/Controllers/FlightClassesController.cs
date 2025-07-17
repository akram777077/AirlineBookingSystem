using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/flight-classes")]
public class FlightClassesController(ISender sender) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightClassById(int id)
    {
        var query = new GetFlightClassByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
