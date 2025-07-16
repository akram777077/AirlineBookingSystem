using AirlineBookingSystem.Shared.Results;
using MediatR;
using System.Collections.Generic;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

public class AssignPermissionsToRoleCommand(int roleId, List<int> permissionIds) : IRequest<Result>
{
    public int RoleId => roleId;
    public List<int> PermissionIds => permissionIds;
}
