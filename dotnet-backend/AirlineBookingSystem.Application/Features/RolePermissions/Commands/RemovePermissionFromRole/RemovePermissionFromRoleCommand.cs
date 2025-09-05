using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

/// <summary>
/// Represents a command to remove a specific permission from a role.
/// </summary>
/// <param name="RoleId">The unique identifier of the role from which the permission will be removed.</param>
/// <param name="PermissionId">The unique identifier of the permission to remove from the role.</param>
public record RemovePermissionFromRoleCommand(int RoleId, int PermissionId) : IRequest<Result>;
