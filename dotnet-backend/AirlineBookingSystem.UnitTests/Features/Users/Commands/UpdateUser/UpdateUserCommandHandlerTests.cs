using AirlineBookingSystem.Application.Features.Users.Commands.UpdateUser;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Users.Commands;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IGenderRepository> _genderRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _genderRepositoryMock = new Mock<IGenderRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _cityRepositoryMock = new Mock<ICityRepository>();

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Genders).Returns(_genderRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Cities).Returns(_cityRepositoryMock.Object);

        _handler = new UpdateUserCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_UpdateUserAndPerson_And_ReturnNoContent()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserCommandWithId(
            userId,
            "updateduser",
            "updated@example.com",
            "Updated",
            "User",
            "Mid",
            new DateTime(1990, 1, 1),
            1,
            "456 Oak St",
            1,
            "54321",
            2
        );

        var existingUser = UserFactory.GetUserFaker(1, 1).Generate();
        existingUser.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        existingUser.Person.Address = AddressFactory.GetAddressFaker(1).Generate();

        var gender = GenderFactory.GetGenderFaker().Generate();
        var role = RoleFactory.GetRoleFaker().Generate();
        var country = CountryFactory.GetCountryFaker().Generate();
        var city = CityFactory.GetCityFaker(country.Id).RuleFor(c => c.Country, country).Generate();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync(gender);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(command.RoleId)).ReturnsAsync(role);
        _cityRepositoryMock.Setup(r => r.GetByIdAsync(command.CityId)).ReturnsAsync(city);

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
        var command = new UpdateUserCommandWithId(
            userId, "user", "email", "first", "last", null, new DateTime(), 1, "street", 1, "zip", 1
        );

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("User not found.", result.Error);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_GenderNotFound()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserCommandWithId(
            userId, "user", "email", "first", "last", null, new DateTime(), 99, "street", 1, "zip", 1
        );

        var existingUser = UserFactory.GetUserFaker(1, 1).Generate();
        existingUser.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        existingUser.Person.Address = AddressFactory.GetAddressFaker(1).Generate();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync((Gender?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Gender not found.", result.Error);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_RoleNotFound()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserCommandWithId(
            userId, "user", "email", "first", "last", null, new DateTime(), 1, "street", 1, "zip", 99
        );

        var existingUser = UserFactory.GetUserFaker(1, 1).Generate();
        existingUser.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        existingUser.Person.Address = AddressFactory.GetAddressFaker(1).Generate();

        var gender = GenderFactory.GetGenderFaker().Generate();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync(gender);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(command.RoleId)).ReturnsAsync((Role?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Role not found.", result.Error);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_CityNotFound()
    {
        // Arrange
        var userId = 1;
        var command = new UpdateUserCommandWithId(
            userId, "user", "email", "first", "last", null, new DateTime(), 1, "street", 99, "zip", 1
        );

        var existingUser = UserFactory.GetUserFaker(1, 1).Generate();
        existingUser.Person = PersonFactory.GetPersonFaker(1, 1).Generate();
        existingUser.Person.Address = AddressFactory.GetAddressFaker(1).Generate();

        var gender = GenderFactory.GetGenderFaker().Generate();
        var role = RoleFactory.GetRoleFaker().Generate();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync(gender);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(command.RoleId)).ReturnsAsync(role);
        _cityRepositoryMock.Setup(r => r.GetByIdAsync(command.CityId)).ReturnsAsync((City?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.Equal("City not found.", result.Error);
    }
}
