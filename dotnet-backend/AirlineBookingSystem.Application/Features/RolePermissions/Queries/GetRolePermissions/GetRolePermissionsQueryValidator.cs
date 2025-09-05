using FluentValidation;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

/// <summary>
/// Validator for the <see cref="GetRolePermissionsQuery"/>.
/// </summary>
public class GetRolePermissionsQueryValidator : AbstractValidator<GetRolePermissionsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetRolePermissionsQueryValidator"/> class.
    /// </summary>
    public GetRolePermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
    }
}
