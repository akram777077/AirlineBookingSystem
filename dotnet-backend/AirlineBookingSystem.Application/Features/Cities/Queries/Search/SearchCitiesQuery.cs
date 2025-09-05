using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.Search;

/// <summary>
/// Represents a query to search for cities based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching cities.</param>
public record SearchCitiesQuery(CitySearchFilter Filter) : IRequest<PagedResult<List<CityDto>>>;
