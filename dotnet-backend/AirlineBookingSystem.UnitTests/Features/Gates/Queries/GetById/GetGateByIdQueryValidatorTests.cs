using AirlineBookingSystem.Application.Features.Gates.Queries.GetById;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Queries.GetById;

public class GetGateByIdQueryValidatorTests
{
    private readonly GetGateByIdQueryValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenIdIsEmpty()
    {
        // Arrange
        var query = new GetGateByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Id" && e.ErrorMessage == "Id is required.");
    }

    [Fact]
    public void Should_NotHaveError_WhenValidId()
    {
        // Arrange
        var query = new GetGateByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
