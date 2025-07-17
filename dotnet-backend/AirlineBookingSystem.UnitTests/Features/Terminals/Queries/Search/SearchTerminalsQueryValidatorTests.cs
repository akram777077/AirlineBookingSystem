using AirlineBookingSystem.Application.Features.Terminals.Queries.Search;
using AirlineBookingSystem.Shared.Filters;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Queries.Search;

public class SearchTerminalsQueryValidatorTests
{
    private readonly SearchTerminalsQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenPageNumberIsZeroOrLess()
    {
        // Arrange
        var filter = new TerminalSearchFilter { PageNumber = 0, PageSize = 10 };
        var query = new SearchTerminalsQuery(filter);

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
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 0 };
        var query = new SearchTerminalsQuery(filter);

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
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 101 };
        var query = new SearchTerminalsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.PageSize" && e.ErrorMessage == "Page size cannot exceed 100.");
    }

    [Fact]
    public void ShouldHaveError_WhenAirportIdIsZeroOrLessAndHasValue()
    {
        // Arrange
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 10, AirportId = 0 };
        var query = new SearchTerminalsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.AirportId" && e.ErrorMessage == "Airport ID must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenNameExceedsMaxLength()
    {
        // Arrange
        var longName = new string('A', 101);
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 10, Name = longName };
        var query = new SearchTerminalsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Filter.Name" && e.ErrorMessage == "Terminal name cannot exceed 100 characters.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 10, AirportId = 1, Name = "Terminal A" };
        var query = new SearchTerminalsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotHaveError_WhenOptionalFieldsAreNull()
    {
        // Arrange
        var filter = new TerminalSearchFilter { PageNumber = 1, PageSize = 10, AirportId = null, Name = null };
        var query = new SearchTerminalsQuery(filter);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
