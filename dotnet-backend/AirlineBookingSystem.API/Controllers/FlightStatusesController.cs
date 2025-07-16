using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/flight-statuses")]
public class FlightStatusesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFlightStatuses()
    {
        var result = await sender.Send(new GetAllFlightStatusesQuery());
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFlightStatusById(int id)
    {
        var query = new GetFlightStatusByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
