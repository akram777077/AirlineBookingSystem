using AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateAirplaneCommandHandler _handler;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;

    public CreateAirplaneCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
    _handler = new CreateAirplaneCommandHandler(_airplaneRepositoryMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirplaneIsCreatedSuccessfully()
    {
        // Arrange
        var createAirplaneDto = new CreateAirplaneDto
        {
            Model = "Boeing 747",
            Manufacturer = "Boeing",
            Capacity = 400,
            Code = "B747"
        };
        var command = new CreateAirplaneCommand(createAirplaneDto);
        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var airplaneDto = new AirplaneDto { Model = "Test Model", Manufacturer = "Test Manufacturer", Capacity = 100, Code = "ABC" };

        _mapperMock.Setup(m => m.Map<Airplane>(createAirplaneDto)).Returns(airplane);
        _airplaneRepositoryMock.Setup(r => r.AddAsync(airplane)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<AirplaneDto>(airplane)).Returns(airplaneDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        result.Value.Should().Be(airplaneDto);
        _airplaneRepositoryMock.Verify(r => r.AddAsync(airplane), Times.Once);
    }
}