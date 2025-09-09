using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Queries.GetById;

public class GetSeatByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenSeatExists()
    {
        // Arrange
        var seatId = 1;
        var seat = new Seat
        {
            Id = seatId,
            ClassTypesId = 1,
            SeatNumber = "1A",
            IsReserved = false,
            AirplaneId = 1,
            ClassType = ClassTypeFactory.GetClassTypeFaker().Generate(),
            Airplane = AirplaneFactory.GetAirplaneFaker().Generate()
        };
        var seatDto = new SeatDto
        {
            Id = seatId,
            ClassTypesId = 1,
            SeatNumber = "1A",
            IsReserved = false,
            AirplaneId = 1
        };

        _unitOfWorkMock.Setup(u => u.Seats.GetByIdAsync(seatId)).ReturnsAsync(seat);
        _mapperMock.Setup(m => m.Map<SeatDto>(seat)).Returns(seatDto);

        var handler = new GetSeatByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetSeatByIdQuery(seatId);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(seatDto);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundResult_WhenSeatDoesNotExist()
    {
        // Arrange
        var seatId = 999;

        _unitOfWorkMock.Setup(u => u.Seats.GetByIdAsync(seatId)).ReturnsAsync((Seat?)null);

        var handler = new GetSeatByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetSeatByIdQuery(seatId);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Contain("Seat not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenExceptionOccurs()
    {
        // Arrange
        var seatId = 1;

        _unitOfWorkMock.Setup(u => u.Seats.GetByIdAsync(seatId)).Throws(new Exception("Database error"));

        var handler = new GetSeatByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var query = new GetSeatByIdQuery(seatId);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Database error");
    }
}