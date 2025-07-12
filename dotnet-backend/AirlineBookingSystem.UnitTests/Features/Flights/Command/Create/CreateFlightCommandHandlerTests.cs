using AirlineBookingSystem.Application.Features.Flights.Command.Create;
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

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.Create;

public class CreateFlightCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateFlightCommandHandler _handler;

    public CreateFlightCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateFlightCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFlightIsCreatedSuccessfully()
    {
        // Arrange
        var createFlightDto = new CreateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new CreateFlightCommand(createFlightDto);     

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();
        var arrivalGate = GateFactory.GetGateFaker(1).Generate();
        var flight = FlightFactory.GetFlightFaker(airplane.Id, arrivalGate.Id, departureGate.Id, (int)FlightStatusEnum.Scheduled).Generate();

        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(createFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.ArrivalGateId.Value))
            .ReturnsAsync(arrivalGate);
        _mapperMock.Setup(m => m.Map<Flight>(createFlightDto))
            .Returns(flight);
        _unitOfWorkMock.Setup(u => u.Flights.IsFlightNumberExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        _unitOfWorkMock.Setup(u => u.Flights.AddAsync(flight))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Created);
        _unitOfWorkMock.Verify(u => u.Flights.AddAsync(flight), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAirplaneNotFound()
    {
        // Arrange
        var createFlightDto = new CreateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new CreateFlightCommand(createFlightDto);

        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(createFlightDto.AirplaneId))
            .ReturnsAsync((Airplane)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airplane not found");
        _unitOfWorkMock.Verify(u => u.Flights.AddAsync(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenDepartureGateNotFound()
    {
        // Arrange
        var createFlightDto = new CreateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new CreateFlightCommand(createFlightDto);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();

        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(createFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.DepartureGateId))
            .ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Departure gate not found");
        _unitOfWorkMock.Verify(u => u.Flights.AddAsync(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenArrivalGateNotFound()
    {
        // Arrange
        var createFlightDto = new CreateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            ArrivalGateId = 2,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new CreateFlightCommand(createFlightDto);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();

        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(createFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.ArrivalGateId.Value))
            .ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Arrival gate not found");
        _unitOfWorkMock.Verify(u => u.Flights.AddAsync(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldGenerateUniqueFlightNumber()
    {
        // Arrange
        var createFlightDto = new CreateFlightDto
        {
            AirplaneId = 1,
            DepartureGateId = 1,
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3)
        };
        var command = new CreateFlightCommand(createFlightDto);

        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var departureGate = GateFactory.GetGateFaker(1).Generate();
        var flight = FlightFactory.GetFlightFaker(airplane.Id, 1, departureGate.Id, (int)FlightStatusEnum.Scheduled).Generate();

        _unitOfWorkMock.Setup(u => u.Airplanes.GetByIdAsync(createFlightDto.AirplaneId))
            .ReturnsAsync(airplane);
        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(createFlightDto.DepartureGateId))
            .ReturnsAsync(departureGate);
        _mapperMock.Setup(m => m.Map<Flight>(createFlightDto))
            .Returns(flight);
        _unitOfWorkMock.SetupSequence(u => u.Flights.IsFlightNumberExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(true) // First call returns true (flight number exists)
            .ReturnsAsync(false); // Second call returns false (unique flight number generated)
        _unitOfWorkMock.Setup(u => u.Flights.AddAsync(flight))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Flights.IsFlightNumberExistsAsync(It.IsAny<string>()), Times.Exactly(2));
        flight.FlightNumber.Should().NotBeNullOrEmpty();
    }
}
