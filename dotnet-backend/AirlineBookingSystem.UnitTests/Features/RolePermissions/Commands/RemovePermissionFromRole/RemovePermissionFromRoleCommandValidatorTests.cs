using AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Commands.RemovePermissionFromRole;

public class RemovePermissionFromRoleCommandValidatorTests
{
    private readonly RemovePermissionFromRoleCommandValidator _validator;

    public RemovePermissionFromRoleCommandValidatorTests()
    {
        _validator = new RemovePermissionFromRoleCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenRoleIdIsZeroOrLess()
    {
        // Arrange
        var command = new RemovePermissionFromRoleCommand(0, 1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "RoleId" && e.ErrorMessage == "Role ID must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenPermissionIdIsZeroOrLess()
    {
        // Arrange
        var command = new RemovePermissionFromRoleCommand(1, 0);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PermissionId" && e.ErrorMessage == "Permission ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var command = new RemovePermissionFromRoleCommand(1, 1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
