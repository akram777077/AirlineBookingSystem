using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Common.Filters;

public class PaginationFilterValidatorTests
{
    private readonly PaginationFilterValidator _validator;

    public PaginationFilterValidatorTests()
    {
        _validator = new PaginationFilterValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenPageSizeExceedsMaximum()
    {
        // Arrange
        var filter = new PaginationFilter { PageNumber = 1, PageSize = 101 };

        // Act
        var result = _validator.Validate(filter);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PageSize" && e.ErrorMessage == "Page size cannot exceed 100.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenPageSizeIsAtMaximum()
    {
        // Arrange
        var filter = new PaginationFilter { PageNumber = 1, PageSize = 100 };

        // Act
        var result = _validator.Validate(filter);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldHaveError_WhenPageNumberIsZeroOrLess()
    {
        // Arrange
        var filter = new PaginationFilter { PageNumber = 0, PageSize = 10 };

        // Act
        var result = _validator.Validate(filter);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PageNumber" && e.ErrorMessage == "Page number must be at least 1.");
    }

    [Fact]
    public void ShouldHaveError_WhenPageSizeIsZeroOrLess()
    {
        // Arrange
        var filter = new PaginationFilter { PageNumber = 1, PageSize = 0 };

        // Act
        var result = _validator.Validate(filter);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PageSize" && e.ErrorMessage == "Page size must be at least 1.");
    }
}