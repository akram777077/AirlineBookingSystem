using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

/// <summary>
/// Handles the removal of a specific permission from a role.
/// </summary>
public class RemovePermissionFromRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RemovePermissionFromRoleCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="RemovePermissionFromRoleCommand"/> to remove a permission from a role.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(RemovePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = await unitOfWork.RolePermissions.GetByRoleIdAndPermissionIdAsync(request.RoleId, request.PermissionId);
        if (rolePermission == null)
        {
            return Result.NotFound("Role permission not found.");
        }

        unitOfWork.RolePermissions.Delete(rolePermission);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}
