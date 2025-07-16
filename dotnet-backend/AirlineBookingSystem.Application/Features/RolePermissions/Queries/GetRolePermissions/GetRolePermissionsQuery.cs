using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

public record GetRolePermissionsQuery(int RoleId) : IRequest<Result<IReadOnlyList<PermissionDto>>>;
