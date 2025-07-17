using AirlineBookingSystem.Shared.DTOs.Cities;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

public record GetCityByIdQuery(int Id) : IRequest<Result<CityDto>>;
