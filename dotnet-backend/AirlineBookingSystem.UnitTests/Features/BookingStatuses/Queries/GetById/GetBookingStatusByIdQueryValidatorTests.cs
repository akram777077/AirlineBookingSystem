using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.BookingStatuses.Queries.GetById;

public class GetBookingStatusByIdQueryValidatorTests
{
    private readonly GetBookingStatusByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenBookingStatusIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetBookingStatusByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenBookingStatusIdIsValid()
    {
        // Arrange
        var query = new GetBookingStatusByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
