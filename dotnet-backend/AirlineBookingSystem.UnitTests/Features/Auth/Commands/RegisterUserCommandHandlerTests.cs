using AirlineBookingSystem.Application.Features.Auth.Commands.Register;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentValidation;

namespace AirlineBookingSystem.UnitTests.Features.Auth.Commands;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IGenderRepository> _genderRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly RegisterUserCommandHandler _handler;
    private readonly RegisterUserCommandValidator _validator;

    public RegisterUserCommandHandlerTests()
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

        _handler = new RegisterUserCommandHandler(_unitOfWorkMock.Object);
        _validator = new RegisterUserCommandValidator();
    }

    [Fact]
    public async Task Handle_Should_CreateUserAndPerson_And_ReturnSuccess()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "password",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        var gender = GenderFactory.GetGenderFaker().Generate();
        var role = RoleFactory.GetRoleFaker().RuleFor(r => r.RoleName, RoleEnum.Customer).Generate();
        var country = CountryFactory.GetCountryFaker().Generate();
        var city = CityFactory.GetCityFaker(country.Id).RuleFor(c => c.Country, country).Generate();

        _genderRepositoryMock.Setup(r => r.GetByIdAsync(command.GenderId)).ReturnsAsync(gender);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync((int)RoleEnum.Customer)).ReturnsAsync(role);
        _cityRepositoryMock.Setup(r => r.GetByIdAsync(command.CityId)).ReturnsAsync(city);

        User capturedUser = null;
        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>()))
            .Callback<User>(user => capturedUser = user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.NotNull(capturedUser);
        Assert.True(BCrypt.Net.BCrypt.Verify(command.Password, capturedUser.Password));
    }

    [Fact]
    public void Validate_Should_Fail_When_UsernameIsEmpty()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "",
            "password",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Username" && e.ErrorMessage == "Username is required.");
    }

    [Fact]
    public void Validate_Should_Fail_When_PasswordIsTooShort()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "short",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must be at least 6 characters long.");
    }

    [Fact]
    public void Validate_Should_Fail_When_PasswordLacksUppercase()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "password123",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must contain at least one uppercase letter.");
    }

    [Fact]
    public void Validate_Should_Fail_When_PasswordLacksLowercase()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "PASSWORD123",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must contain at least one lowercase letter.");
    }

    [Fact]
    public void Validate_Should_Fail_When_PasswordLacksDigit()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "Password!@#",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must contain at least one digit.");
    }

    [Fact]
    public void Validate_Should_Fail_When_PasswordLacksSpecialCharacter()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "Password123",
            "test@example.com",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must contain at least one special character.");
    }

    [Fact]
    public void Validate_Should_Fail_When_EmailIsInvalid()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "Password123!",
            "invalid-email",
            "Test",
            "User",
            null,
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_Should_Fail_When_UserIsUnder18()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "testuser",
            "Password123!",
            "test@example.com",
            "Test",
            "User",
            null,
            DateTime.Today.AddYears(-17),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "DateOfBirth" && e.ErrorMessage == "User must be at least 18 years old.");
    }

    [Fact]
    public void Validate_Should_Pass_When_AllFieldsAreValid()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "validuser",
            "StrongPassword1!",
            "valid@example.com",
            "Valid",
            "User",
            "Mid",
            new DateTime(1990, 1, 1),
            1,
            "123 Main St",
            1,
            "12345"
        );

        // Act
        var validationResult = _validator.Validate(command);

        // Assert
        Assert.True(validationResult.IsValid);
    }
}
