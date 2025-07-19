using AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Commands.AssignPermissionsToRole;

public class AssignPermissionsToRoleCommandValidatorTests
{
    private readonly AssignPermissionsToRoleCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenRoleIdIsZeroOrLess()
    {
        // Arrange
        var command = new AssignPermissionsToRoleCommand(0, new List<int> { 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "RoleId" && e.ErrorMessage == "Role ID must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenPermissionIdsIsNull()
    {
        // Arrange
        var command = new AssignPermissionsToRoleCommand(1, null);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PermissionIds" && e.ErrorMessage == "Permission IDs cannot be null.");
    }

    [Fact]
    public void ShouldHaveError_WhenAnyPermissionIdIsZeroOrLess()
    {
        // Arrange
        var command = new AssignPermissionsToRoleCommand(1, new List<int> { 1, 0, 3 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "All Permission IDs must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var command = new AssignPermissionsToRoleCommand(1, new List<int> { 1, 2, 3 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
