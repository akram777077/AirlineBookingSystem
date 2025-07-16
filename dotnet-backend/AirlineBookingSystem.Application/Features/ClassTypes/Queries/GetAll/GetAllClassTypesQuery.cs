using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;

public record GetAllClassTypesQuery : IRequest<Result<IEnumerable<ClassTypeDto>>>;
