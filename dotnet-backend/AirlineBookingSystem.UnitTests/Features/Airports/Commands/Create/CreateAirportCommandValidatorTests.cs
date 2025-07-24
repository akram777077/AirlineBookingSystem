using AirlineBookingSystem.Application.Features.Airports.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Commands.Create;

public class CreateAirportCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAirportRepository> _airportRepositoryMock;
    private readonly CreateAirportCommandValidator _validator;

    public CreateAirportCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _airportRepositoryMock = new Mock<IAirportRepository>();
        _unitOfWorkMock.Setup(u => u.Airports).Returns(_airportRepositoryMock.Object);
        _validator = new CreateAirportCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenAirportCodeIsEmpty()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("", "Test Airport", 1, "UTC"));
        _airportRepositoryMock.Setup(r => r.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync((Airport)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.AirportCode" && e.ErrorMessage == "AirportCode is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenAirportCodeLengthIsNot3()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABCD", "Test Airport", 1, "UTC"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.AirportCode" && e.ErrorMessage == "AirportCode must be 3 characters long.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenAirportCodeAlreadyExists()
    {
        // Arrange
        var existingAirport = AirportFactory.GetAirportFaker(1).Generate();
        existingAirport.AirportCode = "ABC";
        _airportRepositoryMock.Setup(r => r.GetByCodeAsync("ABC")).ReturnsAsync(existingAirport);
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABC", "Test Airport", 1, "UTC"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.AirportCode" && e.ErrorMessage == "An airport with this code already exists.");
        result.Errors.Should().Contain(e => e.ErrorCode == "Conflict");
    }

    [Fact]
    public async Task ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABC", "", 1, "UTC"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.Name" && e.ErrorMessage == "Name is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCityIdIsZeroOrLess()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABC", "Test Airport", 0, "UTC"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.CityId" && e.ErrorMessage == "CityId must be greater than 0.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenTimezoneIsEmpty()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABC", "Test Airport", 1, ""));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.Timezone" && e.ErrorMessage == "Timezone is required.");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new CreateAirportCommand(new Shared.DTOs.airports.CreateAirportDto("ABC", "Test Airport", 1, "UTC"));
        _airportRepositoryMock.Setup(r => r.GetByCodeAsync("ABC")).ReturnsAsync((Airport)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}