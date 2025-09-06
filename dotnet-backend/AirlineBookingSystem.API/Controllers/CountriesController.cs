using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using AirlineBookingSystem.Application.Features.Countries.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing country-related operations.
/// </summary>
[Route("api/countries")]
[ApiController]
[EnableRateLimiting("fixed")]
public class CountriesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Retrieves all available countries.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="CountryDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all countries.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CountryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCountries()
    {
        var result = await sender.Send(new GetAllCountriesQuery());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves a specific country by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the country.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="CountryDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the country details.</response>
    /// <response code="404">If a country with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
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
