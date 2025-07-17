using AirlineBookingSystem.Application.Features.Cities.Queries.GetById;
using AirlineBookingSystem.Application.Features.Cities.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace AirlineBookingSystem.API.Controllers;

[Route("api/cities")]
[ApiController]
public class CitiesController(ISender sender) : ControllerBase
{
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
