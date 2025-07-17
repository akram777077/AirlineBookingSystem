using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.FlightStatuses.Queries.GetById;

public class GetFlightStatusByIdQueryValidatorTests
{
    private readonly GetFlightStatusByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFlightStatusIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetFlightStatusByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenFlightStatusIdIsValid()
    {
        // Arrange
        var query = new GetFlightStatusByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
