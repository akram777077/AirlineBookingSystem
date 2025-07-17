using AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;
using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Queries.SearchAirplanes;

public class SearchAirplanesQueryValidatorTests
{
    private readonly SearchAirplanesQueryValidator _validator;

    public SearchAirplanesQueryValidatorTests()
    {
        _validator = new SearchAirplanesQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenPageNumberIsZeroOrLess()
    {
        // Arrange
        var filter = new AirplaneSearchFilter { PageNumber = 0, PageSize = 10 };
        var query = new SearchAirplanesQuery(filter);

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
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 0 };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size must be at least 1.");
    }

    [Fact]
    public void ShouldHaveError_WhenPageSizeExceedsMaximum()
    {
        // Arrange
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 101 };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size cannot exceed 100.");
    }

    [Fact]
    public void ShouldHaveError_WhenModelExceedsMaxLength()
    {
        // Arrange
        var longModel = new string('A', 101);
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 10, Model = longModel };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.Model" && e.ErrorMessage == "Model cannot exceed 100 characters.");
    }

    [Fact]
    public void ShouldHaveError_WhenManufacturerExceedsMaxLength()
    {
        // Arrange
        var longManufacturer = new string('A', 101);
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 10, Manufacturer = longManufacturer };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.Manufacturer" && e.ErrorMessage == "Manufacturer cannot exceed 100 characters.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 10, Model = "Boeing 747", Manufacturer = "Boeing" };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotHaveError_WhenOptionalFieldsAreNull()
    {
        // Arrange
        var filter = new AirplaneSearchFilter { PageNumber = 1, PageSize = 10, Model = null, Manufacturer = null };
        var query = new SearchAirplanesQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
