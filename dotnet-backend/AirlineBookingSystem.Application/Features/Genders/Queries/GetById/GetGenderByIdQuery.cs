using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

public record GetGenderByIdQuery(int Id) : IRequest<Result<GenderDto>>;