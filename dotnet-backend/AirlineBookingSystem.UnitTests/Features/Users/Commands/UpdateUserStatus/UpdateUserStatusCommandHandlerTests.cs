using AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Users.Commands;

public class UpdateUserStatusCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UpdateUserStatusCommandHandler _handler;

    public UpdateUserStatusCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _handler = new UpdateUserStatusCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_UpdateUserStatus_And_ReturnNoContent()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserStatusCommand(userId, true);
        var user = UserFactory.GetUserFaker(1, 1).Generate();
        user.IsActive = false; // Set initial state to inactive

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_UserNotFound()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserStatusCommand(userId, true);

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("User not found.", result.Error);
    }
}
