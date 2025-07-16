using AirlineBookingSystem.Application.Features.Gates.Queries.SearchGates;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Queries.SearchGates;

public class SearchGatesQueryValidatorTests
{
    private readonly SearchGatesQueryValidator _validator;

    public SearchGatesQueryValidatorTests()
    {
        _validator = new SearchGatesQueryValidator();
    }

    [Fact]
    public void Should_HaveError_WhenPageNumberIsLessThanOne()
    {
        // Arrange
        var query = new SearchGatesQuery(new GateSearchFilter { PageNumber = 0, PageSize = 10 });

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Filter.PageNumber" && e.ErrorMessage == "Page number must be at least 1.");
    }

    [Fact]
    public void Should_HaveError_WhenPageSizeIsLessThanOne()
    {
        // Arrange
        var query = new SearchGatesQuery(new GateSearchFilter { PageNumber = 1, PageSize = 0 });

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size must be at least 1.");
    }

    [Fact]
    public void Should_HaveError_WhenPageSizeExceedsOneHundred()
    {
        // Arrange
        var query = new SearchGatesQuery(new GateSearchFilter { PageNumber = 1, PageSize = 101 });

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size cannot exceed 100.");
    }

    [Fact]
    public void Should_HaveError_WhenGateNumberExceedsMaxLength()
    {
        // Arrange
        var query = new SearchGatesQuery(new GateSearchFilter { GateNumber = "G12345678901", PageNumber = 1, PageSize = 10 });

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Filter.GateNumber" && e.ErrorMessage == "Gate number cannot exceed 10 characters.");
    }

    [Fact]
    public void Should_NotHaveError_WhenValidFilter()
    {
        // Arrange
        var query = new SearchGatesQuery(new GateSearchFilter { GateNumber = "G10", TerminalId = 1, PageNumber = 1, PageSize = 10 });

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
