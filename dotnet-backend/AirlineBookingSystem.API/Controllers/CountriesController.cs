using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using AirlineBookingSystem.Application.Features.Countries.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await mediator.Send(new GetAllCountriesQuery());
        return Ok(countries);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await mediator.Send(new GetCountryByIdQuery(id));
        return Ok(country);
    }
}
