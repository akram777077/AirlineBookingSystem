using AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Commands.AssignPermissionsToRole;

public class AssignPermissionsToRoleCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AssignPermissionsToRoleCommandHandler _handler;

    public AssignPermissionsToRoleCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AssignPermissionsToRoleCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNoContent_WhenPermissionsAreAssigned()
    {
        // Arrange
        var roleId = 1;
        var permissionIds = new List<int> { 1, 2 };
        var command = new AssignPermissionsToRoleCommand(roleId, permissionIds);
        var role = RoleFactory.GetRoleFaker().Generate();
        var existingPermissions = new List<Permission>();
        var permission1 = PermissionFactory.GetPermissionFaker().Generate();
        permission1.Id = 1;
        var permission2 = PermissionFactory.GetPermissionFaker().Generate();
        permission2.Id = 2;

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync(role);
        _unitOfWorkMock.Setup(u => u.RolePermissions.GetPermissionsByRoleIdAsync(roleId))
            .ReturnsAsync(existingPermissions);
        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(1))
            .ReturnsAsync(permission1);
        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(2))
            .ReturnsAsync(permission2);
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.NoContent);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRoleDoesNotExist()
    {
        // Arrange
        var roleId = 1;
        var permissionIds = new List<int> { 1, 2 };
        var command = new AssignPermissionsToRoleCommand(roleId, permissionIds);

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync((Role?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Role not found.");
        _unitOfWorkMock.Verify(u => u.RolePermissions.AddAsync(It.IsAny<RolePermission>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenPermissionDoesNotExist()
    {
        // Arrange
        var roleId = 1;
        var permissionIds = new List<int> { 1, 99 }; // 99 does not exist
        var command = new AssignPermissionsToRoleCommand(roleId, permissionIds);
        var role = RoleFactory.GetRoleFaker().Generate();
        var existingPermissions = new List<Permission>();
        var permission1 = PermissionFactory.GetPermissionFaker().Generate();
        permission1.Id = 1;

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync(role);
        _unitOfWorkMock.Setup(u => u.RolePermissions.GetPermissionsByRoleIdAsync(roleId))
            .ReturnsAsync(existingPermissions);
        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(1))
            .ReturnsAsync(permission1);
        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(99))
            .ReturnsAsync((Permission?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.BadRequest);
        result.Error.Should().Be("Permission with ID 99 not found.");
    }
}
