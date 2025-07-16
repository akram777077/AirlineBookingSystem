using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

public record GetCountryByIdQuery(int Id) : IRequest<CountryDto>;
