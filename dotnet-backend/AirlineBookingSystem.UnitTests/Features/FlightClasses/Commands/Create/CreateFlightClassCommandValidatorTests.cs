using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Commands.Create;

public class CreateFlightClassCommandValidatorTests
{
    private readonly CreateFlightClassCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var command = new CreateFlightClassCommand(new CreateFlightClassDto
        (
             0, // Empty
             1,
             100m,
             50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateFlightClassDto.FlightId");
    }

    [Fact]
    public void ShouldHaveError_WhenClassIdIsEmpty()
    {
        // Arrange
        var command = new CreateFlightClassCommand(new CreateFlightClassDto
        (
            1,
            0, // Empty
            100m,
            50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateFlightClassDto.ClassId");
    }

    [Fact]
    public void ShouldHaveError_WhenPriceIsZeroOrLess()
    {
        // Arrange
        var command = new CreateFlightClassCommand(new CreateFlightClassDto
        (
            1,
             1,
            0m, // Invalid
            50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateFlightClassDto.Price");
    }

    [Fact]
    public void ShouldHaveError_WhenSeatsIsZeroOrLess()
    {
        // Arrange
        var command = new CreateFlightClassCommand(new CreateFlightClassDto
        (
            1,
            1,
             100m,
             0 // Invalid
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateFlightClassDto.Seats");
    }

    [Fact]
    public void ShouldNotHaveError_WhenDtoIsValid()
    {
        // Arrange
        var command = new CreateFlightClassCommand(new CreateFlightClassDto
        (
            1,
            1,
            100m,
            50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
