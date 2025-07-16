using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;

public record GetPermissionByIdQuery(int Id) : IRequest<Result<PermissionDto>>;