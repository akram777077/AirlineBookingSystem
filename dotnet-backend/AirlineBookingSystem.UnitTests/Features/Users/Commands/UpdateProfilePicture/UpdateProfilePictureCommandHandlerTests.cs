using AirlineBookingSystem.Application.Features.Users.Commands.UpdateProfilePicture;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.UnitTests.Features.Users.Commands;

public class UpdateProfilePictureCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IHostEnvironment> _hostEnvironmentMock;
    private readonly UpdateProfilePictureCommandHandler _handler;

    public UpdateProfilePictureCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _hostEnvironmentMock = new Mock<IHostEnvironment>();

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _hostEnvironmentMock.Setup(h => h.ContentRootPath).Returns(Path.Combine(Directory.GetCurrentDirectory(), "TestRoot"));

        _handler = new UpdateProfilePictureCommandHandler(_unitOfWorkMock.Object, _hostEnvironmentMock.Object);
    }

    [Fact]
    public async Task Handle_Should_UpdateProfilePicture_And_ReturnNoContent()
    {
        // Arrange
        var userId = 1;
        var fileContent = new byte[] { 0x01, 0x02, 0x03 };
        var fileName = "test.jpg";
        var command = new UpdateProfilePictureCommand(userId, fileContent, fileName);

        var user = UserFactory.GetUserFaker(1, 1).Generate();
        user.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        user.Person.ImagePath = null; // No existing image

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NoContent, result.StatusCode);
        Assert.NotNull(user.Person.ImagePath);
        Assert.Contains("/uploads/profile_pictures/", user.Person.ImagePath);
        Assert.EndsWith(".jpg", user.Person.ImagePath);

        // Clean up created test directory and file
        var testRoot = Path.Combine(Directory.GetCurrentDirectory(), "TestRoot");
        if (Directory.Exists(testRoot))
        {
            Directory.Delete(testRoot, true);
        }
    }

    [Fact]
    public async Task Handle_Should_DeleteOldPicture_When_Updating()
    {
        // Arrange
        var userId = 1;
        var fileContent = new byte[] { 0x04, 0x05, 0x06 };
        var fileName = "new.png";
        var command = new UpdateProfilePictureCommand(userId, fileContent, fileName);

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
        Assert.NotNull(user.Person.ImagePath);
        Assert.Contains("/uploads/profile_pictures/", user.Person.ImagePath);
        Assert.EndsWith(".png", user.Person.ImagePath);
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
        var fileContent = new byte[] { 0x01, 0x02, 0x03 };
        var fileName = "test.jpg";
        var command = new UpdateProfilePictureCommand(userId, fileContent, fileName);

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
}