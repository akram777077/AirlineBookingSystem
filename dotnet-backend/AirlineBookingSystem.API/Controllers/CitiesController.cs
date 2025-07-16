using AirlineBookingSystem.Application.Features.Cities.Queries.GetById;
using AirlineBookingSystem.Application.Features.Cities.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] CitySearchFilter filter)
    {
        var cities = await mediator.Send(new SearchCitiesQuery(filter));
        return Ok(cities);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var city = await mediator.Send(new GetCityByIdQuery(id));
        return Ok(city);
    }
}
