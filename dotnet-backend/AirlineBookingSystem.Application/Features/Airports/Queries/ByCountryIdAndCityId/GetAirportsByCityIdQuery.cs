using AirlineBookingSystem.Shared.DTOs.Airports;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;

public record GetAirportsByCityIdQuery(int CityId)
    : IRequest<IReadOnlyCollection<AirportDto>>;