using AirlineBookingSystem.Application.Features.Cities.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Cities;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Cities.Queries.Search;

public class SearchCitiesQueryValidatorTests
{
    private readonly SearchCitiesQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFilterIsNull()
    {
        // Arrange
        var query = new SearchCitiesQuery(null!); 

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter" && e.ErrorMessage == "City search filter cannot be null.");
    }

    [Fact]
    public void ShouldHaveError_WhenPageNumberIsZeroOrLess()
    {
        // Arrange
        var filter = new CitySearchFilter { PageNumber = 0, PageSize = 10 };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageNumber" && e.ErrorMessage == "Page number must be at least 1.");
    }

    [Fact]
    public void ShouldHaveError_WhenPageSizeIsZeroOrLess()
    {
        // Arrange
        var filter = new CitySearchFilter { PageNumber = 1, PageSize = 0 };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size must be at least 1.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenCountryIdIsZeroOrLess(int countryId)
    {
        // Arrange
        var filter = new CitySearchFilter { CountryId = countryId };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.CountryId" && e.ErrorMessage == "Country ID must be greater than zero.");
    }

    [Fact]
    public void ShouldHaveError_WhenNameExceedsMaxLength()
    {
        // Arrange
        var longName = new string('A', 101);
        var filter = new CitySearchFilter { Name = longName };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.Name" && e.ErrorMessage == "City name cannot exceed 100 characters.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenFilterIsValid()
    {
        // Arrange
        var filter = new CitySearchFilter
        {
            PageNumber = 1,
            PageSize = 10,
            CountryId = 1,
            Name = "Test City"
        };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotHaveError_WhenOptionalFieldsAreNull()
    {
        // Arrange
        var filter = new CitySearchFilter
        {
            PageNumber = 1,
            PageSize = 10,
            CountryId = null,
            Name = null
        };
        var query = new SearchCitiesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}