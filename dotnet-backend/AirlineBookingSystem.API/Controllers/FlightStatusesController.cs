using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
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
}
