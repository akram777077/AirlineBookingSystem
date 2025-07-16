using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQuery(int roleId) : IRequest<Result<IReadOnlyList<PermissionDto>>>
{
    public int RoleId => roleId;
}
