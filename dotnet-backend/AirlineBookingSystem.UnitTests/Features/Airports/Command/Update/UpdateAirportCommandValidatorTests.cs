using AirlineBookingSystem.Application.Features.Airports.Commands.Update;
using AirlineBookingSystem.Shared.DTOs.airports;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Command.Update;

public class UpdateAirportCommandValidatorTests
{
    private readonly UpdateAirportCommandValidator _validator;

    public UpdateAirportCommandValidatorTests()
    {
        _validator = new UpdateAirportCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenAirportIdIsZeroOrLess()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 0, AirportCode = "ABC", Name = "Test", CityId = 1, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenAirportCodeIsEmpty()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "", Name = "Test", CityId = 1, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.AirportCode" && e.ErrorMessage == "AirportCode is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenAirportCodeLengthIsNotThree()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "ABCD", Name = "Test", CityId = 1, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.AirportCode" && e.ErrorMessage == "AirportCode must be 3 characters long.");
    }

    [Fact]
    public void ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "ABC", Name = "", CityId = 1, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.Name" && e.ErrorMessage == "Name is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenCityIdIsZeroOrLess()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "ABC", Name = "Test", CityId = 0, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.CityId" && e.ErrorMessage == "CityId must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenTimezoneIsEmpty()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "ABC", Name = "Test", CityId = 1, Timezone = "" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Airport.Timezone" && e.ErrorMessage == "Timezone is required.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var dto = new UpdateAirportDto { Id = 1, AirportCode = "ABC", Name = "Test", CityId = 1, Timezone = "UTC" };
        var command = new UpdateAirportCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}