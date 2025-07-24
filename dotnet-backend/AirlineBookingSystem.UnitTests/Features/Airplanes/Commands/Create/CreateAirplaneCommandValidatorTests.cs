using AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;
    private readonly CreateAirplaneCommandValidator _validator;

    public CreateAirplaneCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
        _validator = new CreateAirplaneCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenModelIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("", "Boeing", 100, "B747"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Model" && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenManufacturerIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "", 100, "B747"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Manufacturer" && e.ErrorMessage == "Manufacturer is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCapacityIsZeroOrLess()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 0, "B747"));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Capacity" && e.ErrorMessage == "Capacity must be greater than 0.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCodeIsEmpty()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 100, ""));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Code" && e.ErrorMessage == "Code is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenCodeFormatIsInvalid()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 100, "ABC1")); // Invalid format
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync((Domain.Entities.Airplane)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CreateAirplaneDto.Code" && e.ErrorMessage == "Code must be 3 capital letters followed by 2 digits (e.g., ABC12).");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new CreateAirplaneCommand(new Shared.DTOs.airplanes.CreateAirplaneDto("Boeing 747", "Boeing", 100, "ABC12"));
        _airplaneRepositoryMock.Setup(r => r.GetByCodeAsync(command.CreateAirplaneDto.Code)).ReturnsAsync((Domain.Entities.Airplane)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}