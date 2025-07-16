using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

public record GetClassTypeByIdQuery(int Id) : IRequest<Result<ClassTypeDto>>;
