using FluentValidation;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

/// <summary>
/// Validator for the <see cref="RemovePermissionFromRoleCommand"/>.
/// </summary>
public class RemovePermissionFromRoleCommandValidator : AbstractValidator<RemovePermissionFromRoleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemovePermissionFromRoleCommandValidator"/> class.
    /// </summary>
    public RemovePermissionFromRoleCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");

        RuleFor(x => x.PermissionId)
            .GreaterThan(0).WithMessage("Permission ID must be greater than 0.");
    }
}
