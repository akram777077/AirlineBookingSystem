using AirlineBookingSystem.Shared.DTOs.Cities;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

public record GetCityByIdQuery(int Id) : IRequest<CityDto>;
