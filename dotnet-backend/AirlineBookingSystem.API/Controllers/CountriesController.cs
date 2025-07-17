using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using AirlineBookingSystem.Application.Features.Countries.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AirlineBookingSystem.API.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CountryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCountries()
    {
        var result = await sender.Send(new GetAllCountriesQuery());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountryById(int id)
    {
        var result = await sender.Send(new GetCountryByIdQuery(id));
        return this.ToActionResult(result);
    }
}
