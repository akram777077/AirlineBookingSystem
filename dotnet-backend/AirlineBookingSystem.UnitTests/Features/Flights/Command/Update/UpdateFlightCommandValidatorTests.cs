using AirlineBookingSystem.Application.Features.Flights.Command.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.Update;

public class UpdateFlightCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateFlightCommandValidator _validator;

    public UpdateFlightCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new UpdateFlightCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new UpdateFlightCommand(0, dto);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenDtoIsNull()
    {
        // Arrange
        var command = new UpdateFlightCommand(1, null);

        // Act
        Func<Task> act = async () => await _validator.ValidateAsync(command);

        // Assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task ShouldHaveError_WhenDepartureTimeIsEarlierThanOriginal()
    {
        // Arrange
        var flightId = 1;
        var originalDepartureTime = DateTimeOffset.UtcNow.AddHours(2);
        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = originalDepartureTime;
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = originalDepartureTime;

        var updateFlightDto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureTime" && e.ErrorMessage == "Departure time cannot be earlier than the original departure time.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenAirplaneIdIsEmpty()
    {
        // Arrange
        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 0,
            DepartureGateId = 1
        };
        var command = new UpdateFlightCommand(1, dto);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.AirplaneId" && e.ErrorMessage == "Airplane ID is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenAirplaneIdIsZeroOrLess()
    {
        // Arrange
        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = -1,
            DepartureGateId = 1
        };
        var command = new UpdateFlightCommand(1, dto);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.AirplaneId" && e.ErrorMessage == "Airplane ID must be greater than zero.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenDepartureGateIdIsEmpty()
    {
        // Arrange
        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 1,
            DepartureGateId = 0
        };
        var command = new UpdateFlightCommand(1, dto);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureGateId" && e.ErrorMessage == "Departure gate ID is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenDepartureGateIdIsZeroOrLess()
    {
        // Arrange
        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 1,
            DepartureGateId = -1
        };
        var command = new UpdateFlightCommand(1, dto);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureGateId" && e.ErrorMessage == "Departure gate ID must be greater than zero.");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var flightId = 1;
        var originalDepartureTime = DateTimeOffset.UtcNow.AddHours(1);
        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = originalDepartureTime;
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = originalDepartureTime;

        var dto = new UpdateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(2),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(4),
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2
        };
        var command = new UpdateFlightCommand(flightId, dto);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}