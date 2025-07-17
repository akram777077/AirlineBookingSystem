using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.FlightStatuses.Queries.GetById;

public class GetFlightStatusByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetFlightStatusByIdQueryHandler _handler;

    public GetFlightStatusByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetFlightStatusByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFlightStatusDto_WhenFlightStatusExists()
    {
        // Arrange
        var flightStatusId = 1;
        var query = new GetFlightStatusByIdQuery(flightStatusId);
        var flightStatus = FlightStatusFactory.GetFlightStatusFaker().Generate();
        flightStatus.Id = flightStatusId;
        var flightStatusDto = new FlightStatusDto { Id = flightStatusId, Name = flightStatus.StatusName.ToString() };

        _unitOfWorkMock.Setup(u => u.FlightStatuses.GetByIdAsync(flightStatusId))
            .ReturnsAsync(flightStatus);
        _mapperMock.Setup(m => m.Map<FlightStatusDto>(flightStatus))
            .Returns(flightStatusDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(flightStatusDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFlightStatusDoesNotExist()
    {
        // Arrange
        var flightStatusId = 1;
        var query = new GetFlightStatusByIdQuery(flightStatusId);

        _unitOfWorkMock.Setup(u => u.FlightStatuses.GetByIdAsync(flightStatusId))
            .ReturnsAsync((FlightStatus?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Flight status not found.");
    }
}
