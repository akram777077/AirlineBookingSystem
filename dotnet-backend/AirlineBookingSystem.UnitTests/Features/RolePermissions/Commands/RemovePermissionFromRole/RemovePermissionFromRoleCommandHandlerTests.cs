using AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Commands.RemovePermissionFromRole;

public class RemovePermissionFromRoleCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RemovePermissionFromRoleCommandHandler _handler;

    public RemovePermissionFromRoleCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RemovePermissionFromRoleCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNoContent_WhenPermissionIsRemoved()
    {
        // Arrange
        var roleId = 1;
        var permissionId = 1;
        var command = new RemovePermissionFromRoleCommand(roleId, permissionId);
        var rolePermission = RolePermissionFactory.GetRolePermissionFaker(roleId, permissionId).Generate();
        _unitOfWorkMock.Setup(u => u.RolePermissions.GetByRoleIdAndPermissionIdAsync(roleId, permissionId))
            .ReturnsAsync(rolePermission);
        _unitOfWorkMock.Setup(u => u.RolePermissions.Delete(rolePermission));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.NoContent);
        _unitOfWorkMock.Verify(u => u.RolePermissions.Delete(rolePermission), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRolePermissionDoesNotExist()
    {
        // Arrange
        var roleId = 1;
        var permissionId = 1;
        var command = new RemovePermissionFromRoleCommand(roleId, permissionId);

        _unitOfWorkMock.Setup(u => u.RolePermissions.GetByRoleIdAndPermissionIdAsync(roleId, permissionId))
            .ReturnsAsync((RolePermission?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Role permission not found.");
        _unitOfWorkMock.Verify(u => u.RolePermissions.Delete(It.IsAny<RolePermission>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}
