using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ById;

public class GetAirportByIdHandler (IAirportRepository airportRepository, IMapper mapper)
    : IRequestHandler<GetAirportByIdQuery, AirportDto?>
{
    public async Task<AirportDto?> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
    {
        var airport = await airportRepository.GetByIdAsync(request.Id);
        return mapper.Map<AirportDto>(airport);
    }
}