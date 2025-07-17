using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AutoMapper;
using Moq;
using Xunit;
using FluentAssertions;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Queries.GetById;

public class GetFlightClassByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetFlightClassByIdQueryHandler _handler;

    public GetFlightClassByIdQueryHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetFlightClassByIdQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GivenValidId_ReturnsSuccessResultWithFlightClassDto()
    {
        // Arrange
        var flightClassId = 1;
        var flightClass = FlightClassFactory.GetFlightClassFaker(1, 1).Generate();
        flightClass.Id = flightClassId;
        flightClass.Seats = SeatFactory.GetSeatFaker(flightClassId).Generate(flightClass.SeatCapacity / 2); // Half seats taken

        var flightClassDto = new FlightClassDto
        {
            Id = flightClassId,
            FlightId = flightClass.FlightId,
            ClassTypeId = flightClass.ClassTypeId,
            Price = flightClass.Price,
            TotalSeats = flightClass.SeatCapacity,
            AvailableSeats = flightClass.SeatCapacity - flightClass.Seats.Count
        };

        _mockUnitOfWork.Setup(u => u.FlightClasses.GetByIdAsync(flightClassId))
            .ReturnsAsync(flightClass);
        _mockMapper.Setup(m => m.Map<FlightClassDto>(flightClass))
            .Returns(flightClassDto);

        var query = new GetFlightClassByIdQuery(flightClassId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(flightClassDto);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var flightClassId = 999;

        _mockUnitOfWork.Setup(u => u.FlightClasses.GetByIdAsync(flightClassId))
            .ReturnsAsync((FlightClass)null);

        var query = new GetFlightClassByIdQuery(flightClassId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("FlightClass not found.");
    }
}
