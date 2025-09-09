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
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto { Model = "", Manufacturer = "Boeing", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Model" && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenManufacturerIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto { Model = "Boeing 747", Manufacturer = "", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Manufacturer" && e.ErrorMessage == "Manufacturer is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenCapacityIsZeroOrLess()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 0, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Capacity" && e.ErrorMessage == "Capacity must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenCodeIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 100, Code = "" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Code" && e.ErrorMessage == "Code is required.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}