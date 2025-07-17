using AirlineBookingSystem.Application.Features.Flights.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.Update;

public class UpdateFlightCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateFlightCommandHandler _handler;

    public UpdateFlightCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateFlightCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFlightIsUpdatedSuccessfully()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = DateTimeOffset.UtcNow.AddDays(1);
        existingFlight.ArrivalTime = DateTimeOffset.UtcNow.AddDays(1).AddHours(2);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();
        var arrivalGate = GateFactory.GetGateFaker(1).Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.ArrivalGateId.Value))
            .ReturnsAsync(arrivalGate);
        _mapperMock.Setup(m => m.Map(updateFlightDto, existingFlight));
        _unitOfWorkMock.Setup(u => u.Flights.Update(existingFlight));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.NoContent);
        _unitOfWorkMock.Verify(u => u.Flights.Update(existingFlight), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFlightNotFound()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto();
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync((Flight)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Flight not found");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAirplaneNotFound()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync((Airplane)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airplane not found");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenDepartureGateNotFound()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;
        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.DepartureGateId))
            .ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Departure gate not found");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenArrivalGateNotFound()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;
        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.ArrivalGateId.Value))
            .ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Arrival gate not found");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldSetFlightStatusToDelayed_WhenDepartureTimeIsIncreased()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            DepartureTime = DateTimeOffset.UtcNow.AddDays(2),
            ArrivalTime = DateTimeOffset.UtcNow.AddDays(2).AddHours(2)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = DateTimeOffset.UtcNow.AddDays(1);
        existingFlight.ArrivalTime = DateTimeOffset.UtcNow.AddDays(1).AddHours(2);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _mapperMock.Setup(m => m.Map(updateFlightDto, existingFlight));
        _unitOfWorkMock.Setup(u => u.Flights.Update(existingFlight));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        existingFlight.FlightStatusId.Should().Be((int)FlightStatusEnum.Delayed);
    }

    [Fact]
    public async Task Handle_ShouldSetFlightStatusToDelayed_WhenArrivalTimeIsIncreased()
    {
        // Arrange
        var flightId = 1;
        var updateFlightDto = new UpdateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            DepartureTime = DateTimeOffset.UtcNow.AddDays(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddDays(1).AddHours(3)
        };
        var command = new UpdateFlightCommand(flightId, updateFlightDto);

        var existingFlight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        existingFlight.Id = flightId;
        existingFlight.DepartureTime = DateTimeOffset.UtcNow.AddDays(1);
        existingFlight.ArrivalTime = DateTimeOffset.UtcNow.AddDays(1).AddHours(2);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(existingFlight);
        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(updateFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(updateFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _mapperMock.Setup(m => m.Map(updateFlightDto, existingFlight));
        _unitOfWorkMock.Setup(u => u.Flights.Update(existingFlight));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        existingFlight.FlightStatusId.Should().Be((int)FlightStatusEnum.Delayed);
    }
}