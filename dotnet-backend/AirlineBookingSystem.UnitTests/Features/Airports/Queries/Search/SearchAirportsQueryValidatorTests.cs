using AirlineBookingSystem.Application.Features.Airports.Queries.Search;
using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries.Search;

public class SearchAirportsQueryValidatorTests
{
    private readonly SearchAirportsQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenPageNumberIsZeroOrLess()
    {
        // Arrange
        var filter = new AirportSearchFilter { PageNumber = 0, PageSize = 10 };
        var query = new SearchAirportsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageNumber" && e.ErrorMessage == "PageNumber at least greater than or equal to 1.");
    }

    [Fact]
    public void ShouldHaveError_WhenPageSizeIsZeroOrLess()
    {
        // Arrange
        var filter = new AirportSearchFilter { PageNumber = 1, PageSize = 0 };
        var query = new SearchAirportsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "PageSize at least greater than or equal to 1.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var filter = new AirportSearchFilter { PageNumber = 1, PageSize = 10 };
        var query = new SearchAirportsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}