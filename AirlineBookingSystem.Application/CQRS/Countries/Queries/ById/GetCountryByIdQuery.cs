using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.CQRS.Countries.Queries.ById;

public record GetCountryByIdQuery(int Id) : IRequest<CountryDto?>;