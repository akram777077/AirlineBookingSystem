using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Commands.Update;

public class UpdateFlightClassCommandValidatorTests
{
    private readonly UpdateFlightClassCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenIdIsEmpty()
    {
        // Arrange
        var command = new UpdateFlightClassCommand(new UpdateFlightClassDto
        (
             0, // Empty
             100m,
             50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateFlightClassDto.Id");
    }

    [Fact]
    public void ShouldHaveError_WhenPriceIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateFlightClassCommand(new UpdateFlightClassDto
        (
            1,
            0m, // Invalid
            50
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateFlightClassDto.Price");
    }

    [Fact]
    public void ShouldHaveError_WhenSeatsIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateFlightClassCommand(new UpdateFlightClassDto
        (
            1,
             100m,
             0 // Invalid
        ));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateFlightClassDto.Seats");
    }

    [Fact]
    public void ShouldNotHaveError_WhenDtoIsValid()
    {
        // Arrange
        var command = new UpdateFlightClassCommand(new UpdateFlightClassDto
        (
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