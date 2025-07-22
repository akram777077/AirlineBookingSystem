using AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Queries.GetAvailableSeats;

public class GetAvailableSeatsQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenSeatsAreFound()
    {
        // Arrange
        var flightId = 1;
        var classTypeId = 1;
        var seats = new List<Seat>
        {
            SeatFactory.GetSeatFaker(classTypeId).Generate(),
            SeatFactory.GetSeatFaker(classTypeId).Generate()
        };
        var seatDtos = new List<SeatDto>
        {
            new SeatDto { Id = 1, ClassTypesId = classTypeId, SeatNumber = "1A", IsReserved = false, AirplaneId = 1 },
            new SeatDto { Id = 2, ClassTypesId = classTypeId, SeatNumber = "1B", IsReserved = false, AirplaneId = 1 }
        };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId)).ReturnsAsync(ClassTypeFactory.GetClassTypeFaker().Generate());
        _unitOfWorkMock.Setup(u => u.Seats.GetAvailableSeatsAsync(flightId, classTypeId)).ReturnsAsync(seats);
        _mapperMock.Setup(m => m.Map<List<SeatDto>>(seats)).Returns(seatDtos);

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(seatDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenNoClassTypeIsProvided()
    {
        // Arrange
        var flightId = 1;
        int? classTypeId = null;
        var seats = new List<Seat>
        {
            SeatFactory.GetSeatFaker(1).Generate(),
            SeatFactory.GetSeatFaker(2).Generate()
        };
        var seatDtos = new List<SeatDto>
        {
            new SeatDto { Id = 1, ClassTypesId = 1, SeatNumber = "1A", IsReserved = false, AirplaneId = 1 },
            new SeatDto { Id = 2, ClassTypesId = 2, SeatNumber = "1B", IsReserved = false, AirplaneId = 1 }
        };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());
        _unitOfWorkMock.Setup(u => u.Seats.GetAvailableSeatsAsync(flightId, classTypeId)).ReturnsAsync(seats);
        _mapperMock.Setup(m => m.Map<List<SeatDto>>(seats)).Returns(seatDtos);

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(seatDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoSeatsAreFound()
    {
        // Arrange
        var flightId = 1;
        var classTypeId = 1;
        var seats = new List<Seat>();
        var seatDtos = new List<SeatDto>();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId)).ReturnsAsync(ClassTypeFactory.GetClassTypeFaker().Generate());
        _unitOfWorkMock.Setup(u => u.Seats.GetAvailableSeatsAsync(flightId, classTypeId)).ReturnsAsync(seats);
        _mapperMock.Setup(m => m.Map<List<SeatDto>>(seats)).Returns(seatDtos);

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenExceptionOccurs()
    {
        // Arrange
        var flightId = 1;
        var classTypeId = 1;

        _unitOfWorkMock.Setup(u => u.Seats.GetAvailableSeatsAsync(flightId, classTypeId)).Throws(new Exception("Database error"));
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId)).ReturnsAsync(ClassTypeFactory.GetClassTypeFaker().Generate());

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Database error");
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundResult_WhenFlightDoesNotExist()
    {
        // Arrange
        var flightId = 999;
        var classTypeId = 1;

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync((Flight)null);

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Contain($"Flight with ID {flightId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundResult_WhenClassTypeDoesNotExist()
    {
        // Arrange
        var flightId = 1;
        var classTypeId = 999;

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId)).ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate()); // Flight exists
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId)).ReturnsAsync((ClassType)null);

        var handler = new GetAvailableSeatsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetAvailableSeatsQuery(new GetAvailableSeatsFilterDto { FlightId = flightId, ClassTypeId = classTypeId });

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Contain($"ClassType with ID {classTypeId} not found.");
    }
}