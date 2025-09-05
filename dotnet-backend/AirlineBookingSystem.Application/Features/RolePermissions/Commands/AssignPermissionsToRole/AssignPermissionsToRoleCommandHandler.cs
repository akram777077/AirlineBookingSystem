using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

/// <summary>
/// Handles the assignment and removal of permissions for a specific role.
/// </summary>
public class AssignPermissionsToRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AssignPermissionsToRoleCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="AssignPermissionsToRoleCommand"/> to assign permissions to a role.
    /// This method adds new permissions to a role and removes existing permissions that are no longer specified.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            return Result.NotFound("Role not found.");
        }

        var existingPermissions = await unitOfWork.RolePermissions.GetPermissionsByRoleIdAsync(request.RoleId);
        var existingPermissionIds = existingPermissions.Select(p => p.Id).ToHashSet();

        var permissionsToAdd = request.PermissionIds!
            .Except(existingPermissionIds)
            .ToList();

        var permissionsToRemove = existingPermissionIds
            .Except(request.PermissionIds!)
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
