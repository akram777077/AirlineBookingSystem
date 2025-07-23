using AirlineBookingSystem.Application.Features.Auth.Queries.Login;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Auth.Queries;

public class LoginUserQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly LoginUserQueryHandler _handler;

    public LoginUserQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _handler = new LoginUserQueryHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_CredentialsAreValid()
    {
        // Arrange
        var username = "testuser";
        var password = "password";
        var query = new LoginUserQuery(username, password);
        var user = UserFactory.GetUserFaker(1, 1).RuleFor(u => u.Username, username).RuleFor(u => u.Password, BCrypt.Net.BCrypt.HashPassword(password)).Generate();

        _userRepositoryMock.Setup(r => r.GetUserWithPersonAsync(username)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetUserWithPersonAsync(username), Times.Once);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_UserNotFound()
    {
        // Arrange
        var username = "nonexistentuser";
        var password = "password";
        var query = new LoginUserQuery(username, password);

        _userRepositoryMock.Setup(r => r.GetUserWithPersonAsync(username)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetUserWithPersonAsync(username), Times.Once);
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Handle_Should_ReturnUnauthorized_When_InvalidPassword()
    {
        // Arrange
        var username = "testuser";
        var password = "wrongpassword";
        var query = new LoginUserQuery(username, password);
        var user = UserFactory.GetUserFaker(1, 1).RuleFor(u => u.Username, username).RuleFor(u => u.Password, BCrypt.Net.BCrypt.HashPassword("correctpassword")).Generate();

        _userRepositoryMock.Setup(r => r.GetUserWithPersonAsync(username)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetUserWithPersonAsync(username), Times.Once);
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.Unauthorized, result.StatusCode);
    }
}
