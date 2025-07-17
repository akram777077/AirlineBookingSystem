using AirlineBookingSystem.Application.Features.Flights.Queries.GetById;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries.GetById;

public class GetFlightByIdQueryValidatorTests
{
    private readonly GetFlightByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var command = new GetFlightByIdQuery(0);

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
        var command = new GetFlightByIdQuery(-1);

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
        var command = new GetFlightByIdQuery(1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}