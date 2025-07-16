using AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.FlightStatuses.Queries.GetAll;

public class GetAllFlightStatusesQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllFlightStatusesQueryHandler _handler;

    public GetAllFlightStatusesQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllFlightStatusesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllFlightStatuses()
    {
        // Arrange
        var flightStatuses = new List<FlightStatus>
        {
            FlightStatusFactory.GetFlightStatusFaker().Generate(),
            FlightStatusFactory.GetFlightStatusFaker().Generate()
        };
        var flightStatusDtos = new List<FlightStatusDto>
        {
            new FlightStatusDto { Id = flightStatuses[0].Id, Name = flightStatuses[0].StatusName.ToString() },
            new FlightStatusDto { Id = flightStatuses[1].Id, Name = flightStatuses[1].StatusName.ToString() }
        };

        _unitOfWorkMock.Setup(u => u.FlightStatuses.GetAllAsync()).ReturnsAsync(flightStatuses);
        _mapperMock.Setup(m => m.Map<IEnumerable<FlightStatusDto>>(flightStatuses)).Returns(flightStatusDtos);

        // Act
        var result = await _handler.Handle(new GetAllFlightStatusesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(flightStatusDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoFlightStatusesExist()
    {
        // Arrange
        var flightStatuses = new List<FlightStatus>();
        var flightStatusDtos = new List<FlightStatusDto>();

        _unitOfWorkMock.Setup(u => u.FlightStatuses.GetAllAsync()).ReturnsAsync(flightStatuses);
        _mapperMock.Setup(m => m.Map<IEnumerable<FlightStatusDto>>(flightStatuses)).Returns(flightStatusDtos);

        // Act
        var result = await _handler.Handle(new GetAllFlightStatusesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
