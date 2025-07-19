using AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryValidatorTests
{
    private readonly GetRolePermissionsQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenRoleIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetRolePermissionsQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "RoleId" && e.ErrorMessage == "Role ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenRoleIdIsValid()
    {
        // Arrange
        var query = new GetRolePermissionsQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
