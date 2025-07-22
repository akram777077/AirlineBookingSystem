using AirlineBookingSystem.Application.Features.Seats.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Commands.Create;

public class CreateSeatCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenSeatIsCreated()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto
        {
            ClassTypesId = 1,
            SeatNumber = "1A",
            IsReserved = false,
            AirplaneId = 1
        });
        var seat = SeatFactory.GetSeatFaker(1).Generate();

        _mapperMock.Setup(m => m.Map<Seat>(command.Seat)).Returns(seat);

        _unitOfWorkMock.Setup(u => u.Seats.AddAsync(It.IsAny<Seat>()))
            .Callback<Seat>(s => s.Id = 1);

        var handler = new CreateSeatCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);
        _unitOfWorkMock.Verify(u => u.Seats.AddAsync(It.IsAny<Seat>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenExceptionOccurs()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto
        {
            ClassTypesId = 1,
            SeatNumber = "1A",
            IsReserved = false,
            AirplaneId = 1
        });

        _mapperMock.Setup(m => m.Map<Seat>(command.Seat)).Throws(new Exception("Mapping error"));

        var handler = new CreateSeatCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Mapping error");
        _unitOfWorkMock.Verify(u => u.Seats.AddAsync(It.IsAny<Seat>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}