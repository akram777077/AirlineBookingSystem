using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCode;

public class GetAirportByCodeHandler (IAirportRepository airportRepository, IMapper mapper)
    : IRequestHandler<GetAirportByCodeQuery, AirportDto?>
{
    public async Task<AirportDto?> Handle(GetAirportByCodeQuery request, CancellationToken cancellationToken)
    {
        var airport = await airportRepository.GetByCodeAsync(request.Code);
        return mapper.Map<AirportDto>(airport);
    }
}