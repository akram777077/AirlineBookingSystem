using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

public class AssignPermissionsToRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AssignPermissionsToRoleCommand, Result>
{
    public async Task<Result> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            return Result.NotFound("Role not found.");
        }

        var existingPermissions = await unitOfWork.RolePermissions.GetPermissionsByRoleIdAsync(request.RoleId);
        var existingPermissionIds = existingPermissions.Select(p => p.Id).ToHashSet();

        var permissionsToAdd = request.PermissionIds
            .Except(existingPermissionIds)
            .ToList();

        var permissionsToRemove = existingPermissionIds
            .Except(request.PermissionIds)
            .ToList();

        foreach (var permissionId in permissionsToAdd)
        {
            var permission = await unitOfWork.Permissions.GetByIdAsync(permissionId);
            if (permission == null)
            {
                return Result.Failure("Permission with ID " + permissionId + " not found.", ResultStatusCode.BadRequest);
            }
            await unitOfWork.RolePermissions.AddAsync(new RolePermission { RoleId = request.RoleId, PermissionId = permissionId, Role = role, Permission = permission });
        }

        foreach (var permissionId in permissionsToRemove)
        {
            var rolePermission = await unitOfWork.RolePermissions.GetByRoleIdAndPermissionIdAsync(request.RoleId, permissionId);
            if (rolePermission != null)
            {
                unitOfWork.RolePermissions.Delete(rolePermission);
            }
        }

        await unitOfWork.CompleteAsync();
        return Result.Success(ResultStatusCode.NoContent);
    }
}
