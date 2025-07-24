using AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentAssertions;
using Moq;
using FluentValidation.Results;
using FluentValidation;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.Update;

public class UpdateAirplaneCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;
    private readonly UpdateAirplaneCommandValidator _validator;

    public UpdateAirplaneCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
        _validator = new UpdateAirplaneCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenIdIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(0, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenModelIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("", "Boeing", 100, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Model" && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenManufacturerIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "", 100, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Manufacturer" && e.ErrorMessage == "Manufacturer is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCapacityIsZeroOrLess()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 0, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Capacity" && e.ErrorMessage == "Capacity must be greater than 0.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCodeIsEmpty()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, ""));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Code" && e.ErrorMessage == "Code is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCodeFormatIsInvalid()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, "ABC1")); // Invalid format
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync((Domain.Entities.Airplane)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Code" && e.ErrorMessage == "Code must be 3 capital letters followed by 2 digits (e.g., ABC12).");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCodeAlreadyExistsForAnotherAirplane()
    {
        // Arrange
        var existingAirplane = new Airplane { Id = 2, Code = "ABC12", Model = "Existing Model", Manufacturer = "Existing Manufacturer" };
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync("ABC12")).ReturnsAsync(existingAirplane);
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UpdateAirplaneDto.Code" && e.ErrorMessage == "An airplane with this code already exists.");
        result.Errors.Should().Contain(e => e.ErrorCode == "Conflict");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, "ABC12"));
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync(command.UpdateAirplaneDto.Code)).ReturnsAsync((Domain.Entities.Airplane)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenCodeExistsForSameAirplane()
    {
        // Arrange
        var existingAirplane = new Airplane { Id = 1, Code = "ABC12", Model = "Existing Model", Manufacturer = "Existing Manufacturer" };
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync("ABC12")).ReturnsAsync(existingAirplane);
        var command = new UpdateAirplaneCommand(1, new Shared.DTOs.airplanes.UpdateAirplaneDto("Boeing 747", "Boeing", 100, "ABC12"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}