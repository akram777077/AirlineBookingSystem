using AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Permissions.Queries.GetAll;

public class GetAllPermissionsQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllPermissionsQueryHandler _handler;

    public GetAllPermissionsQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllPermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllPermissions()
    {
        // Arrange
        var permissions = PermissionFactory.GetPermissionFaker().Generate(3);
        var permissionDtos = permissions.Select(p => new PermissionDto { Id = p.Id, Name = p.Name.ToString() }).ToList();

        _unitOfWorkMock.Setup(u => u.Permissions.GetAllAsync())
            .ReturnsAsync(permissions);
        _mapperMock.Setup(m => m.Map<IReadOnlyList<PermissionDto>>(permissions))
            .Returns(permissionDtos);

        // Act
        var result = await _handler.Handle(new GetAllPermissionsQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissionDtos);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }
}
