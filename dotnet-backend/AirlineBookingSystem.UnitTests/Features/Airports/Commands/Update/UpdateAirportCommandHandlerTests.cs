using AirlineBookingSystem.Application.Features.Airports.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Commands.Update;

public class UpdateAirportCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateAirportCommandHandler _handler;

    public UpdateAirportCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateAirportCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirportIsUpdatedSuccessfully()
    {
        // Arrange
        var airportId = 1;
        var updateAirportDto = new UpdateAirportDto
        {
            Id = airportId,
            AirportCode = "ABC",
            Name = "Updated Airport",
            CityId = 1,
            Timezone = "UTC"
        };
        var command = new UpdateAirportCommand(updateAirportDto);

        var existingAirport = AirportFactory.GetAirportFaker(1).Generate();
        existingAirport.Id = airportId;

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(airportId))
            .ReturnsAsync(existingAirport);
        _mapperMock.Setup(m => m.Map(updateAirportDto, existingAirport));
        _unitOfWorkMock.Setup(u => u.Airports.Update(existingAirport));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        _unitOfWorkMock.Verify(u => u.Airports.Update(existingAirport), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAirportNotFound()
    {
        // Arrange
        var airportId = 1;
        var updateAirportDto = new UpdateAirportDto
        {
            Id = airportId,
            AirportCode = "ABC",
            Name = "Updated Airport",
            CityId = 1,
            Timezone = "UTC"
        };
        var command = new UpdateAirportCommand(updateAirportDto);

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(airportId))
            .ReturnsAsync((Airport?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airport not found.");
        _unitOfWorkMock.Verify(u => u.Airports.Update(It.IsAny<Airport>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}