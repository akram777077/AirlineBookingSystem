using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.FlightStatuses.Queries.GetAll;

public class GetAllFlightStatusesQueryValidatorTests
{
    private readonly GetAllFlightStatusesQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllFlightStatusesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}