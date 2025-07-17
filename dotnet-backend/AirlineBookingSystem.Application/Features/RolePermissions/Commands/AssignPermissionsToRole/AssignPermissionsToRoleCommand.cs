using AirlineBookingSystem.Shared.Results;
using MediatR;
using System.Collections.Generic;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

public record AssignPermissionsToRoleCommand(int RoleId, List<int>? PermissionIds) : IRequest<Result>;
