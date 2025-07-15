using AirlineBookingSystem.Application.Features.Airplanes.Commands.UpdateAirplane;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.UpdateAirplane;

public class UpdateAirplaneCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateAirplaneCommandHandler _handler;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;

    public UpdateAirplaneCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
        _handler = new UpdateAirplaneCommandHandler(_airplaneRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirplaneIsUpdatedSuccessfully()
    {
        // Arrange
        var airplaneId = 1;
        var updateAirplaneDto = new UpdateAirplaneDto
        {
            Model = "Boeing 747 Updated",
            Manufacturer = "Boeing",
            Capacity = 450,
            Code = "B747"
        };
        var command = new UpdateAirplaneCommand(airplaneId, updateAirplaneDto);
        var existingAirplane = AirplaneFactory.GetAirplaneFaker().Generate();
        existingAirplane.Id = airplaneId;
        var airplaneDto = new AirplaneDto { Model = "Test Model", Manufacturer = "Test Manufacturer", Capacity = 100, Code = "ABC" };

        _airplaneRepositoryMock.Setup(r => r.GetByIdAsync(airplaneId)).ReturnsAsync(existingAirplane);
        _mapperMock.Setup(m => m.Map(updateAirplaneDto, existingAirplane));
        _airplaneRepositoryMock.Setup(r => r.Update(existingAirplane));
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<AirplaneDto>(existingAirplane)).Returns(airplaneDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        result.Value.Should().Be(airplaneDto);
        _airplaneRepositoryMock.Verify(r => r.Update(existingAirplane), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAirplaneNotFound()
    {
        // Arrange
        var airplaneId = 1;
        var updateAirplaneDto = new UpdateAirplaneDto { Model = "Test Model", Manufacturer = "Test Manufacturer", Capacity = 100, Code = "ABC" };
        var command = new UpdateAirplaneCommand(airplaneId, updateAirplaneDto);

        _airplaneRepositoryMock.Setup(r => r.GetByIdAsync(airplaneId)).ReturnsAsync((Airplane)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airplane not found.");
        _airplaneRepositoryMock.Verify(r => r.Update(It.IsAny<Airplane>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}