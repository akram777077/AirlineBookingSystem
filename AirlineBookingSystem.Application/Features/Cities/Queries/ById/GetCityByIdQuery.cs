using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ById;

public record GetCityByIdQuery(int Id) : IRequest<CityDto?>;