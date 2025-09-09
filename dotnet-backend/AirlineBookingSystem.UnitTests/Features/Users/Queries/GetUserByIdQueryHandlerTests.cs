using AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Users.Queries;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _handler = new GetUserByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnUser_When_UserExists()
    {
        // Arrange
        var userId = 1;
        var query = new GetUserByIdQuery(userId);
        var user = UserFactory.GetUserFaker(1, 1).Generate();
        var userDto = new UserDto { Id = userId, Username = "testuser", Email = "test@example.com", FirstName = "Test", LastName = "User", IsActive = true, RoleName = "Customer" };

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
        _mapperMock.Verify(m => m.Map<UserDto>(user), Times.Once);
        Assert.True(result.IsSuccess);
        result.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_UserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var query = new GetUserByIdQuery(userId);

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
    }
}
