using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.All;

public class GetAllFlightsQueryHandler (IFlightService flightService, IMapper mapper)
    : IRequestHandler<GetAllFlightsQuery, IReadOnlyCollection<FlightDto>>
{
    public async Task<IReadOnlyCollection<FlightDto>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await flightService.GetAllAsync();
        return mapper.Map<List<FlightDto>>(flights);
    }
}