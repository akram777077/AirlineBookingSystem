using AirlineBookingSystem.Application.Features.Countries.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries.GetById;

public class GetCountryByIdQueryValidatorTests
{
    private readonly GetCountryByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenCountryIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetCountryByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenCountryIdIsValid()
    {
        // Arrange
        var query = new GetCountryByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
