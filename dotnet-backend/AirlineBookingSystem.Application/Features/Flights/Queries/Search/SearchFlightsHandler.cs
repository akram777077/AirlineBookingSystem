using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.Search;

public class SearchFlightsHandler (IFlightRepository repository,IMapper mapper)
    : IRequestHandler<SearchFlightsQuery, IReadOnlyCollection<FlightDto>>
{
    public async Task<IReadOnlyCollection<FlightDto>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
       var flights = await repository.SearchFlightsAsync(request.FromCode, request.ToCode, request.DateTime);
         return mapper.Map<List<FlightDto>>(flights);
    }
}