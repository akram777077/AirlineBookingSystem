using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Query.Search;

public class SearchFlightsQuery(FlightSearchFilter filter) : IRequest<Result<IReadOnlyList<FlightSearchResultDto>>>
{
    public FlightSearchFilter Filter => filter;
}