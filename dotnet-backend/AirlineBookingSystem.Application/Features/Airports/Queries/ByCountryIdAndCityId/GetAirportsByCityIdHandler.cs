using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;

public class GetAirportsByCityIdHandler (IAirportRepository repo, IMapper mapper)
    : IRequestHandler<GetAirportsByCityIdQuery, IReadOnlyCollection<AirportDto>>
{
    public async Task<IReadOnlyCollection<AirportDto>> Handle(GetAirportsByCityIdQuery request, CancellationToken cancellationToken)
    {
        var airports = await repo.GetByCityIdAsync(request.CityId);
        return mapper.Map<IReadOnlyCollection<AirportDto>>(airports);
    }
}