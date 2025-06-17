using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.All;

public record GetAllCountriesQuery() : IRequest<IReadOnlyCollection<CountryDto>>;