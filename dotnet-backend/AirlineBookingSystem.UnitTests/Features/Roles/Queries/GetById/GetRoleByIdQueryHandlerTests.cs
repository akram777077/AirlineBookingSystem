using AirlineBookingSystem.Application.Features.Roles.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Roles.Queries.GetById;

public class GetRoleByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetRoleByIdQueryHandler _handler;

    public GetRoleByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetRoleByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnRoleDto_WhenRoleExists()
    {
        // Arrange
        var roleId = 1;
        var query = new GetRoleByIdQuery(roleId);
        var role = RoleFactory.GetRoleFaker().Generate();
        role.Id = roleId;
        var roleDto = new RoleDto { Id = roleId, Name = role.RoleName.ToString() };

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync(role);
        _mapperMock.Setup(m => m.Map<RoleDto>(role))
            .Returns(roleDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(roleDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRoleDoesNotExist()
    {
        // Arrange
        var roleId = 1;
        var query = new GetRoleByIdQuery(roleId);

        _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(roleId))
            .ReturnsAsync((Role)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Role not found.");
    }
}
