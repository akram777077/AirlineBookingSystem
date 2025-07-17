using AirlineBookingSystem.Application.Features.Flights.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries.GetById;

public class GetFlightByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetFlightByIdQueryHandler _handler;

    public GetFlightByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetFlightByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFlightDetailsDto_WhenFlightExists()
    {
        // Arrange
        var flightId = 1;
        var command = new GetFlightByIdQuery(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        var flightDetailsDto = new FlightDetailsDto(); // Assuming a default constructor or properties can be set

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);
        _mapperMock.Setup(m => m.Map<FlightDetailsDto>(flight))
            .Returns(flightDetailsDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(flightDetailsDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFlightDoesNotExist()
    {
        // Arrange
        var flightId = 1;
        var command = new GetFlightByIdQuery(flightId);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync((Flight)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Flight not found");
    }
}