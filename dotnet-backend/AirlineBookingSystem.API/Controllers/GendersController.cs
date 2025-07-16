using AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/genders")]
public class GendersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GenderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllGenders()
    {
        var result = await sender.Send(new GetAllGendersQuery());
        return this.ToActionResult(result);
    }
}
