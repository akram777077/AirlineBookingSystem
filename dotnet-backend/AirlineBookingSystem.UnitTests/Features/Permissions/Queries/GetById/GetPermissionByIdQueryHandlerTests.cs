using AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Permissions.Queries.GetById;

public class GetPermissionByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetPermissionByIdQueryHandler _handler;

    public GetPermissionByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetPermissionByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissionDto_WhenPermissionExists()
    {
        // Arrange
        var permissionId = 1;
        var query = new GetPermissionByIdQuery(permissionId);
        var permission = PermissionFactory.GetPermissionFaker().Generate();
        permission.Id = permissionId;
        var permissionDto = new PermissionDto { Id = permissionId, Name = permission.Name.ToString() };

        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(permissionId))
            .ReturnsAsync(permission);
        _mapperMock.Setup(m => m.Map<PermissionDto>(permission))
            .Returns(permissionDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(permissionDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenPermissionDoesNotExist()
    {
        // Arrange
        var permissionId = 1;
        var query = new GetPermissionByIdQuery(permissionId);

        _unitOfWorkMock.Setup(u => u.Permissions.GetByIdAsync(permissionId))
            .ReturnsAsync((Permission)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Permission not found.");
    }
}
