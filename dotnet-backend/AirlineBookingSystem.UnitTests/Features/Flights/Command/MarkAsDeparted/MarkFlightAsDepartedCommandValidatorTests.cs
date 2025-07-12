using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsDeparted;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.MarkAsDeparted;

public class MarkFlightAsDepartedCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MarkFlightAsDepartedCommandValidator _validator;

    public MarkFlightAsDepartedCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new MarkFlightAsDepartedCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var command = new MarkFlightAsDepartedCommand(0);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightIdIsZeroOrLess()
    {
        // Arrange
        var command = new MarkFlightAsDepartedCommand(-1);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID must be greater than zero.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenDepartureTimeIsInTheFuture()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsDepartedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        flight.Id = flightId;
        flight.DepartureTime = DateTimeOffset.UtcNow.AddHours(1);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight cannot be marked as departed before its scheduled departure time.");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenFlightIdIsValidAndDepartureTimeIsInThePast()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsDepartedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        flight.Id = flightId;
        flight.DepartureTime = DateTimeOffset.UtcNow.AddHours(-1);
        flight.FlightNumber = "FL123";
        flight.Airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        flight.ArrivalGate = GateFactory.GetGateFaker(1).Generate();
        flight.DepartureGate = GateFactory.GetGateFaker(1).Generate();
        flight.FlightStatus = FlightStatusFactory.GetFlightStatusFaker().Generate();
        flight.FlightNumber = "FL123";
        flight.Airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        flight.ArrivalGate = GateFactory.GetGateFaker(1).Generate();
        flight.DepartureGate = GateFactory.GetGateFaker(1).Generate();
        flight.FlightStatus = FlightStatusFactory.GetFlightStatusFaker().Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}