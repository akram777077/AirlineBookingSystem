using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/booking-statuses")]
public class BookingStatusesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBookingStatuses()
    {
        var result = await sender.Send(new GetAllBookingStatusesQuery());
        return this.ToActionResult(result);
    }
}
