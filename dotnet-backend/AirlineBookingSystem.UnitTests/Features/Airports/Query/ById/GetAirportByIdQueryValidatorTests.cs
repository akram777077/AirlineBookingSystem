using AirlineBookingSystem.Application.Features.Airports.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Query.ById;

public class GetAirportByIdQueryValidatorTests
{
    private readonly GetAirportByIdQueryValidator _validator;

    public GetAirportByIdQueryValidatorTests()
    {
        _validator = new GetAirportByIdQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenAirportIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetAirportByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAirportIdIsValid()
    {
        // Arrange
        var query = new GetAirportByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}