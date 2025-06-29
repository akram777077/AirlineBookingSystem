using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.ById;

public class GetFlightByIdHandler (IFlightService flightService, IMapper mapper)
    : IRequestHandler<GetFlightByIdQuery, FlightDto?>
{
    public async Task<FlightDto?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        var flight = await flightService.GetByIdAsync(request.Id);
        return flight is null ? null : mapper.Map<FlightDto>(flight);
    }
}