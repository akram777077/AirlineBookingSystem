using AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Queries.GetByFlightId;

public class GetFlightClassesByFlightIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetFlightClassesByFlightIdQueryHandler _handler;

    public GetFlightClassesByFlightIdQueryHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetFlightClassesByFlightIdQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GivenValidFlightId_ReturnsSuccessResultWithFlightClassDtos()
    {
        // Arrange
        var flightId = 1;
        var flightClasses = new List<FlightClass>
        {
            FlightClassFactory.GetFlightClassFaker(flightId, 1).Generate(),
            FlightClassFactory.GetFlightClassFaker(flightId, 2).Generate()
        };

        var flightClassDtos = new List<FlightClassDto>
        {
            new FlightClassDto
            {
                Id = flightClasses[0].Id,
                FlightId = flightClasses[0].FlightId,
                ClassTypeId = flightClasses[0].ClassTypeId,
                Price = flightClasses[0].Price,
                TotalSeats = flightClasses[0].SeatCapacity,
                AvailableSeats = flightClasses[0].SeatCapacity
            },
            new FlightClassDto
            {
                Id = flightClasses[1].Id,
                FlightId = flightClasses[1].FlightId,
                ClassTypeId = flightClasses[1].ClassTypeId,
                Price = flightClasses[1].Price,
                TotalSeats = flightClasses[1].SeatCapacity,
                AvailableSeats = flightClasses[1].SeatCapacity
            }
        };

        _mockUnitOfWork.Setup(u => u.FlightClasses.GetAllAsync())
            .ReturnsAsync(flightClasses);
        _mockMapper.Setup(m => m.Map<IEnumerable<FlightClassDto>>(flightClasses))
            .Returns(flightClassDtos);

        var query = new GetFlightClassesByFlightIdQuery(flightId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(flightClassDtos);
    }

    [Fact]
    public async Task Handle_GivenInvalidFlightId_ReturnsNotFoundResult()
    {
        // Arrange
        var flightId = 999;

        _mockUnitOfWork.Setup(u => u.FlightClasses.GetAllAsync())
            .ReturnsAsync(new List<FlightClass>());

        var query = new GetFlightClassesByFlightIdQuery(flightId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be($"No flight classes found for FlightId: {flightId}.");
    }
}
