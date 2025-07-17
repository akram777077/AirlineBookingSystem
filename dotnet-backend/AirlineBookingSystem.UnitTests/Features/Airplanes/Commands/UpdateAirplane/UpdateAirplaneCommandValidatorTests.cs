using AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.UpdateAirplane;

public class UpdateAirplaneCommandValidatorTests
{
    private readonly UpdateAirplaneCommandValidator _validator;

    public UpdateAirplaneCommandValidatorTests()
    {
        _validator = new UpdateAirplaneCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenIdIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(0, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenModelIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "", Manufacturer = "Boeing", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Model" && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenManufacturerIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "Boeing 747", Manufacturer = "", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Manufacturer" && e.ErrorMessage == "Manufacturer is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenCapacityIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 0, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Capacity" && e.ErrorMessage == "Capacity must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenCodeIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 100, Code = "" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Code" && e.ErrorMessage == "Code is required.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto { Model = "Boeing 747", Manufacturer = "Boeing", Capacity = 100, Code = "B747" });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}