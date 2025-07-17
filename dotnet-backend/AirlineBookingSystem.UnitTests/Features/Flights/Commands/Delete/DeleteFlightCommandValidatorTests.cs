using AirlineBookingSystem.Application.Features.Flights.Commands.Delete;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Commands.Delete;

public class DeleteFlightCommandValidatorTests
{
    private readonly DeleteFlightCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var command = new DeleteFlightCommand(0);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenFlightIdIsZeroOrLess()
    {
        // Arrange
        var command = new DeleteFlightCommand(-1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID must be greater than zero.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenFlightIdIsValid()
    {
        // Arrange
        var command = new DeleteFlightCommand(1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}