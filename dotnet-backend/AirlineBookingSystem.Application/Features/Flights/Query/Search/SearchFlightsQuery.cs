using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Query.Search;


public record SearchFlightsQuery(FlightSearchFilter Filter) : IRequest<PagedResult<List<FlightSearchResultDto>>>;
