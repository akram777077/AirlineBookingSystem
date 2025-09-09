using AirlineBookingSystem.Application.Features.Users.Commands.CreateUser;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Users.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IGenderRepository> _genderRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
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

        _handler = new CreateUserCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_CreateUserAndPerson_And_ReturnSuccess()
    {
        // Arrange
        var command = new CreateUserCommand(
            "newuser",
            "password123",
            "newuser@example.com",
            "New",
            "User",
            null,
            new DateTime(1995, 5, 10),
            1,
            "456 Oak Ave",
            1,
            "54321",
            2 // Example RoleId
        );

        var gender = GenderFactory.GetGenderFaker().Generate();
        var role = RoleFactory.GetRoleFaker().Generate();
        var country = CountryFactory.GetCountryFaker().Generate();
        var city = CityFactory.GetCityFaker(country.Id).RuleFor(c => c.Country, country).Generate();

        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync(gender);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(command.RoleId)).ReturnsAsync(role);
        _cityRepositoryMock.Setup(r => r.GetByIdAsync(command.CityId)).ReturnsAsync(city);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Username == command.Username)), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_When_GenderNotFound()
    {
        // Arrange
        var command = new CreateUserCommand(
            "newuser", "password123", "newuser@example.com", "New", "User", null,
            new DateTime(1995, 5, 10), 99, "456 Oak Ave", 1, "54321", 2
        );

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
        var command = new CreateUserCommand(
            "newuser", "password123", "newuser@example.com", "New", "User", null,
            new DateTime(1995, 5, 10), 1, "456 Oak Ave", 1, "54321", 99
        );

        var gender = GenderFactory.GetGenderFaker().Generate();
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
        var command = new CreateUserCommand(
            "newuser", "password123", "newuser@example.com", "New", "User", null,
            new DateTime(1995, 5, 10), 1, "456 Oak Ave", 99, "54321", 2
        );

        var gender = GenderFactory.GetGenderFaker().Generate();
        var role = RoleFactory.GetRoleFaker().Generate();
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