using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;

public record GetAllPermissionsQuery : IRequest<Result<IReadOnlyList<PermissionDto>>>;