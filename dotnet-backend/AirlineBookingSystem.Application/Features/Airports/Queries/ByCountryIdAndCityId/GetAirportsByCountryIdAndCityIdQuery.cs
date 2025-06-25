using AirlineBookingSystem.Shared.DTOs.Airports;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;

public record GetAirportsByCountryIdAndCityIdQuery(int CountryId, int CityId)
    : IRequest<IReadOnlyCollection<AirportDto>>;