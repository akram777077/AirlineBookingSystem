using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.FlightClasses.Commands.Create;

public class CreateFlightClassCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateFlightClassCommandHandler _handler;

    public CreateFlightClassCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
    _handler = new CreateFlightClassCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenFlightClassIsCreatedSuccessfully()
    {
        // Arrange
        var createFlightClassDto = new CreateFlightClassDto
        (
            1,
             1,
           100m,
            50
        );
        var command = new CreateFlightClassCommand(createFlightClassDto);

        var flight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();
        var classType = ClassTypeFactory.GetClassTypeFaker().Generate();
        var flightClass = new FlightClass { Id = 1, FlightId = 1, ClassTypeId = 1, Price = 100m, SeatCapacity = 50, Flight = flight, ClassType = classType };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(flight);
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(classType);
        _mapperMock.Setup(m => m.Map<FlightClass>(It.IsAny<CreateFlightClassDto>()))
            .Returns(flightClass);
        _unitOfWorkMock.Setup(u => u.FlightClasses.AddAsync(It.IsAny<FlightClass>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(flightClass.Id);
        _unitOfWorkMock.Verify(u => u.Flights.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.ClassTypes.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mapperMock.Verify(m => m.Map<FlightClass>(It.IsAny<CreateFlightClassDto>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.FlightClasses.AddAsync(It.IsAny<FlightClass>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenFlightNotFound()
    {
        // Arrange
        var createFlightClassDto = new CreateFlightClassDto
        (
             1,
            1,
             100m,
            50
        );
        var command = new CreateFlightClassCommand(createFlightClassDto);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Flight?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
            result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Flight not found");
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        _unitOfWorkMock.Verify(u => u.Flights.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.ClassTypes.GetByIdAsync(It.IsAny<int>()), Times.Never);
        _mapperMock.Verify(m => m.Map<FlightClass>(It.IsAny<CreateFlightClassDto>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.FlightClasses.AddAsync(It.IsAny<FlightClass>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenClassTypeNotFound()
    {
        // Arrange
        var createFlightClassDto = new CreateFlightClassDto
        (
             1,
             1,
             100m,
             50
        );
        var command = new CreateFlightClassCommand(createFlightClassDto);

        var flight = FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate();

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(flight);
        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((ClassType?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("ClassType not found");
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        _unitOfWorkMock.Verify(u => u.Flights.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.ClassTypes.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mapperMock.Verify(m => m.Map<FlightClass>(It.IsAny<CreateFlightClassDto>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.FlightClasses.AddAsync(It.IsAny<FlightClass>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}
