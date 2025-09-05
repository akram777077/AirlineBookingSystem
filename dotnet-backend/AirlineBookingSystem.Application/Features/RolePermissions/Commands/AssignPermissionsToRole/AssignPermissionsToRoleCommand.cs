using AirlineBookingSystem.Shared.Results;
using MediatR;
using System.Collections.Generic;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

/// <summary>
/// Represents a command to assign permissions to a specific role.
/// </summary>
/// <param name="RoleId">The unique identifier of the role to which permissions will be assigned.</param>
/// <param name="PermissionIds">A list of unique identifiers of permissions to assign to the role.</param>
public record AssignPermissionsToRoleCommand(int RoleId, List<int>? PermissionIds) : IRequest<Result>;
