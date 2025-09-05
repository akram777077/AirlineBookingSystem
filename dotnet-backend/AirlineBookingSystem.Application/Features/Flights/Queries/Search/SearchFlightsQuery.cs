using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.Search;

/// <summary>
/// Represents a query to search for flights based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching flights.</param>
public record SearchFlightsQuery(FlightSearchFilter Filter) : IRequest<PagedResult<List<FlightSearchResultDto>>>;

