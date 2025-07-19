using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.BookingStatuses.Queries.GetAll;

public class GetAllBookingStatusesQueryValidatorTests
{
    private readonly GetAllBookingStatusesQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllBookingStatusesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
