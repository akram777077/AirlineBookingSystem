using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

public class RemovePermissionFromRoleCommand(int roleId, int permissionId) : IRequest<Result>
{
    public int RoleId => roleId;
    public int PermissionId => permissionId;
}
