using AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Roles.Queries.GetAll;

public class GetAllRolesQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllRolesQueryHandler _handler;

    public GetAllRolesQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllRolesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllRoles()
    {
        // Arrange
        var roles = RoleFactory.GetRoleFaker().Generate(3);
        var roleDtos = roles.Select(r => new RoleDto { Id = r.Id, Name = r.RoleName.ToString() }).ToList();

        _unitOfWorkMock.Setup(u => u.Roles.GetAllAsync())
            .ReturnsAsync(roles);
        _mapperMock.Setup(m => m.Map<IReadOnlyList<RoleDto>>(roles))
            .Returns(roleDtos);

        // Act
        var result = await _handler.Handle(new GetAllRolesQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(roleDtos);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }
}
