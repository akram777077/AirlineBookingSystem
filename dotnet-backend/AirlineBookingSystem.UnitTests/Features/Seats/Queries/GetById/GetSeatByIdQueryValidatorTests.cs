using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Queries.GetById;

public class GetSeatByIdQueryValidatorTests
{
    private readonly GetSeatByIdQueryValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenIdIsZero()
    {
        // Arrange
        var query = new GetSeatByIdQuery(0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_NotHaveError_WhenIdIsValid()
    {
        // Arrange
        var query = new GetSeatByIdQuery(1);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}