using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;

public record GetCitiesByCountryIdQuery(int CountryId) : IRequest<IReadOnlyCollection<CityDto>>;