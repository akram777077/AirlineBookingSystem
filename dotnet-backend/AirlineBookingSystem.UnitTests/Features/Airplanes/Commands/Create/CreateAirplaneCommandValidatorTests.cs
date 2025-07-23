using AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandValidatorTests
{
    private readonly CreateAirplaneCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenModelIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("", "Boeing", 100, "B747"));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Model" && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenManufacturerIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "", 100, "B747"));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Manufacturer" && e.ErrorMessage == "Manufacturer is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenCapacityIsZeroOrLess()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 0, "B747"));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Capacity" && e.ErrorMessage == "Capacity must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenCodeIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 100, ""));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Code" && e.ErrorMessage == "Code is required.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 100, "B747"));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}