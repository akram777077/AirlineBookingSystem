using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Commands.Update;

public class UpdateFlightClassCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateFlightClassCommandHandler _handler;

    public UpdateFlightClassCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateFlightClassCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenFlightClassIsUpdatedSuccessfully()
    {
        // Arrange
        var updateFlightClassDto = new UpdateFlightClassDto
        (
            1,
           100m,
            50
        );
        var command = new UpdateFlightClassCommand(updateFlightClassDto);

        var flightClass = FlightClassFactory.GetFlightClassFaker(1,1).Generate();

        _unitOfWorkMock.Setup(u => u.FlightClasses.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(flightClass);
        _mapperMock.Setup(m => m.Map(It.IsAny<UpdateFlightClassDto>(), It.IsAny<FlightClass>()))
            .Returns(flightClass);
        _unitOfWorkMock.Setup(u => u.FlightClasses.Update(It.IsAny<FlightClass>()));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(flightClass.Id);
        _unitOfWorkMock.Verify(u => u.FlightClasses.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<UpdateFlightClassDto>(), It.IsAny<FlightClass>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.FlightClasses.Update(It.IsAny<FlightClass>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenFlightClassNotFound()
    {
        // Arrange
        var updateFlightClassDto = new UpdateFlightClassDto
        (
             1,
             100m,
            50
        );
        var command = new UpdateFlightClassCommand(updateFlightClassDto);

        _unitOfWorkMock.Setup(u => u.FlightClasses.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((FlightClass?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
    result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("FlightClass not found");
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        _unitOfWorkMock.Verify(u => u.FlightClasses.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<UpdateFlightClassDto>(), It.IsAny<FlightClass>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.FlightClasses.Update(It.IsAny<FlightClass>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}