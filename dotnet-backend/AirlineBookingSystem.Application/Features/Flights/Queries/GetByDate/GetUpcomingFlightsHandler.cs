using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetByDate;

public class GetUpcomingFlightsHandler (IFlightService flightService, IMapper mapper)
    : IRequestHandler<GetUpcomingFlightsQuery, IReadOnlyCollection<FlightDto>>
{
    public async Task<IReadOnlyCollection<FlightDto>> Handle(GetUpcomingFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await flightService.GetUpcomingFlightsAsync(request.DateTime);
        return mapper.Map<List<FlightDto>>(flights);
    }
}