using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetByDate;

public class GetUpcomingFlightsHandler (IFlightRepository flightRepository,IMapper mapper)
    : IRequestHandler<GetUpcomingFlightsQuery, IReadOnlyCollection<FlightDto>>
{
    public async Task<IReadOnlyCollection<FlightDto>> Handle(GetUpcomingFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await flightRepository.GetUpcomingFlightsAsync(request.DateTime);
        return mapper.Map<List<FlightDto>>(flights);
    }
}