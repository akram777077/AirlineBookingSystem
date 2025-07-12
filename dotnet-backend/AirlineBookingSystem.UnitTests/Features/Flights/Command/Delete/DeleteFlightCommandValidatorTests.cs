using AirlineBookingSystem.Application.Features.Flights.Command.Delete;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.Delete;

public class DeleteFlightCommandValidatorTests
{
    private readonly DeleteFlightCommandValidator _validator;

    public DeleteFlightCommandValidatorTests()
    {
        _validator = new DeleteFlightCommandValidator();
    }

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