using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetUpcomingFlightsByDateTime;

public class GetUpcomingFlightsByDateTimeHandler (IFlightRepository flightRepository,IMapper mapper)
    : IRequestHandler<GetUpcomingFlightsByDateTimeQuery, IReadOnlyCollection<FlightDto>>
{
    public async Task<IReadOnlyCollection<FlightDto>> Handle(GetUpcomingFlightsByDateTimeQuery request, CancellationToken cancellationToken)
    {
        var flights = await flightRepository.GetUpcomingFlightsAsync(request.DateTime);
        return mapper.Map<List<FlightDto>>(flights);
    }
}