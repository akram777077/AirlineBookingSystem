using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.ById;

public class GetFlightByIdHandler (IFlightRepository flightRepository ,IMapper mapper)
    : IRequestHandler<GetFlightByIdQuery, FlightDto?>
{
    public async Task<FlightDto?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        var flight = await flightRepository.GetByIdAsync(request.Id);
        return flight is null ? null : mapper.Map<FlightDto>(flight);
    }
}