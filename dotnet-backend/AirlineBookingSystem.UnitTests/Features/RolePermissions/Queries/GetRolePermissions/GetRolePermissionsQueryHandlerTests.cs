using AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetRolePermissionsQueryHandler _handler;

    public GetRolePermissionsQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetRolePermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissions_WhenRoleExists()
    {
        // Arrange
        var roleId = 1;
        var query = new GetRolePermissionsQuery(roleId);
        var role = RoleFactory.GetRoleFaker().Generate();
        var permissions = PermissionFactory.GetPermissionFaker().Generate(3);
        var permissionDtos = permissions.Select(p => new PermissionDto { Id = p.Id, Name = p.Name.ToString() }).ToList();

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync(role);
        _unitOfWorkMock.Setup(u => u.RolePermissions.GetPermissionsByRoleIdAsync(roleId))
            .ReturnsAsync(permissions);
        _mapperMock.Setup(m => m.Map<IReadOnlyList<PermissionDto>>(permissions))
            .Returns(permissionDtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissionDtos);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRoleDoesNotExist()
    {
        // Arrange
        var roleId = 1;
        var query = new GetRolePermissionsQuery(roleId);

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync((Role?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Role not found.");
    }
}
