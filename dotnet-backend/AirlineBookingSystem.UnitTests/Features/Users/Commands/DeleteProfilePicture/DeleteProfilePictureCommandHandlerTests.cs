using AirlineBookingSystem.Application.Features.Users.Commands.DeleteProfilePicture;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.UnitTests.Features.Users.Commands;

public class DeleteProfilePictureCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IHostEnvironment> _hostEnvironmentMock;
    private readonly DeleteProfilePictureCommandHandler _handler;

    public DeleteProfilePictureCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _hostEnvironmentMock = new Mock<IHostEnvironment>();

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _hostEnvironmentMock.Setup(h => h.ContentRootPath).Returns(Path.Combine(Directory.GetCurrentDirectory(), "TestRoot"));

        _handler = new DeleteProfilePictureCommandHandler(_unitOfWorkMock.Object, _hostEnvironmentMock.Object);
    }

    [Fact]
    public async Task Handle_Should_DeleteProfilePicture_And_ReturnNoContent()
    {
        // Arrange
        var userId = 1;
        var command = new DeleteProfilePictureCommand(userId);

        var oldFileName = Guid.NewGuid().ToString() + ".jpg";
        var oldFilePath = Path.Combine(_hostEnvironmentMock.Object.ContentRootPath, "wwwroot", "uploads", "profile_pictures", oldFileName);
        var directory = Path.GetDirectoryName(oldFilePath);
        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }
        await File.WriteAllBytesAsync(oldFilePath, new byte[] { 0x01, 0x02, 0x03 });

        var user = UserFactory.GetUserFaker(1, 1).Generate();
        user.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        user.Person.ImagePath = Path.Combine("/uploads", "profile_pictures", oldFileName).Replace('\\', '/');

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NoContent, result.StatusCode);
        Assert.Null(user.Person.ImagePath);
        Assert.False(File.Exists(oldFilePath));

        // Clean up created test directory and file
        var testRoot = Path.Combine(Directory.GetCurrentDirectory(), "TestRoot");
        if (Directory.Exists(testRoot))
        {
            Directory.Delete(testRoot, true);
        }
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_UserNotFound()
    {
        // Arrange
        var userId = 1;
        var command = new DeleteProfilePictureCommand(userId);

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("User not found.", result.Error);

        // Clean up created test directory and file
        var testRoot = Path.Combine(Directory.GetCurrentDirectory(), "TestRoot");
        if (Directory.Exists(testRoot))
        {
            Directory.Delete(testRoot, true);
        }
    }

    [Fact]
    public async Task Handle_Should_ReturnNoContent_When_NoProfilePictureExists()
    {
        // Arrange
        var userId = 1;
        var command = new DeleteProfilePictureCommand(userId);

        var user = UserFactory.GetUserFaker(1, 1).Generate();
        user.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        user.Person.ImagePath = null; // No existing image

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never); // Should not update if no image
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never); // Should not complete if no image
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NoContent, result.StatusCode);
        Assert.Null(user.Person.ImagePath);

        // Clean up created test directory and file
        var testRoot = Path.Combine(Directory.GetCurrentDirectory(), "TestRoot");
        if (Directory.Exists(testRoot))
        {
            Directory.Delete(testRoot, true);
        }
    }
}