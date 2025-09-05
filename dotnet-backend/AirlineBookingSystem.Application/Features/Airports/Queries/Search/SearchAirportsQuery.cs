using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.Search;

/// <summary>
/// Represents a query to search for airports based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching airports.</param>
public record SearchAirportsQuery(AirportSearchFilter Filter) : IRequest<PagedResult<List<AirportSearchResultDto>>>;