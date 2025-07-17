using AirlineBookingSystem.Application.Features.Flights.Queries.Search;
using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries.Search;

public class SearchFlightsQueryValidatorTests
{
    private readonly SearchFlightsQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFilterIsNull()
    {
        // Arrange
        var query = new SearchFlightsQuery(null!); // Use null-forgiving operator as Filter is required

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter" && e.ErrorMessage == "Flight search filter cannot be null.");
    }

    [Fact]
    public void ShouldHaveError_WhenDepartureDateIsInThePast()
    {
        // Arrange
        var filter = new FlightSearchFilter { DepartureDate = DateTimeOffset.UtcNow.AddDays(-1) };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.DepartureDate" && e.ErrorMessage == "Departure date cannot be in the past.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenFromCityIdIsZeroOrLess(int cityId)
    {
        // Arrange
        var filter = new FlightSearchFilter { FromCityId = cityId };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.FromCityId" && e.ErrorMessage == "From City ID must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenToCityIdIsZeroOrLess(int cityId)
    {
        // Arrange
        var filter = new FlightSearchFilter { ToCityId = cityId };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.ToCityId" && e.ErrorMessage == "To City ID must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenFromCountryIdIsZeroOrLess(int countryId)
    {
        // Arrange
        var filter = new FlightSearchFilter { FromCountryId = countryId };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.FromCountryId" && e.ErrorMessage == "From Country ID must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenToCountryIdIsZeroOrLess(int countryId)
    {
        // Arrange
        var filter = new FlightSearchFilter { ToCountryId = countryId };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.ToCountryId" && e.ErrorMessage == "To Country ID must be greater than zero.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenFilterIsValid()
    {
        // Arrange
        var filter = new FlightSearchFilter
        {
            DepartureDate = DateTimeOffset.UtcNow.AddDays(1),
            FromCityId = 1,
            ToCityId = 2,
            FromCountryId = 1,
            ToCountryId = 2
        };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotHaveError_WhenOptionalFieldsAreNull()
    {
        // Arrange
        var filter = new FlightSearchFilter
        {
            DepartureDate = null,
            FromCityId = null,
            ToCityId = null,
            FromCountryId = null,
            ToCountryId = null
        };
        var query = new SearchFlightsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}