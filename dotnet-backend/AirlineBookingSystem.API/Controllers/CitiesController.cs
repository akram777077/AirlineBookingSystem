using AirlineBookingSystem.Application.Features.Cities.Queries.GetById;
using AirlineBookingSystem.Application.Features.Cities.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing city-related operations.
/// </summary>
[Route("api/cities")]
[ApiController]
public class CitiesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Searches for cities based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="filter">An object containing search parameters for cities, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{CityDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of cities matching the criteria.</response>
    /// <response code="400">If the search filter is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<CityDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchCities([FromQuery] CitySearchFilter filter)
    {
        var result = await sender.Send(new SearchCitiesQuery(filter));

        if (result.IsSuccess && result is { } pagedResult)
        {
            var routeValues = new RouteValueDictionary(filter.ToDictionary().Select(x => new KeyValuePair<string, object?>(x.Key, x.Value)));

            if (pagedResult.PageNumber < pagedResult.TotalPages)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber + 1;
                pagedResult.Metadata["nextPageUri"] = Url.Link(null, routeValues)!;
            }

            if (pagedResult.PageNumber > 1)
            {
                routeValues["pageNumber"] = pagedResult.PageNumber - 1;
                pagedResult.Metadata["prevPageUri"] = Url.Link(null, routeValues)!;
            }
        }

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves a specific city by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the city.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="CityDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the city details.</response>
    /// <response code="404">If a city with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCityById(int id)
    {
        var result = await sender.Send(new GetCityByIdQuery(id));
        return this.ToActionResult(result);
    }
}
