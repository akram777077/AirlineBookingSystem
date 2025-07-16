using AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Queries.GetById;

public class GetAirplaneByIdQueryValidatorTests
{
    private readonly GetAirplaneByIdQueryValidator _validator;

    public GetAirplaneByIdQueryValidatorTests()
    {
        _validator = new GetAirplaneByIdQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetAirplaneByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenIdIsValid()
    {
        // Arrange
        var query = new GetAirplaneByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}