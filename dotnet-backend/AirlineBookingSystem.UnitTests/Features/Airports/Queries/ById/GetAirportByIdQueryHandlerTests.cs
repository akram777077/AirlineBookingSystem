using AirlineBookingSystem.Application.Features.Airports.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries.ById;

public class GetAirportByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAirportByIdQueryHandler _handler;

    public GetAirportByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAirportByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAirportDto_WhenAirportExists()
    {
        // Arrange
        var airportId = 1;
        var query = new GetAirportByIdQuery(airportId);
        var airport = AirportFactory.GetAirportFaker(1).Generate();
        airport.Id = airportId;
        var airportDto = new AirportDto { Id = airportId, Name = airport.Name, AirportCode = airport.AirportCode, CityId = airport.CityId, Timezone = airport.Timezone };

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(airportId))
            .ReturnsAsync(airport);
        _mapperMock.Setup(m => m.Map<AirportDto>(airport))
            .Returns(airportDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(airportDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAirportDoesNotExist()
    {
        // Arrange
        var airportId = 1;
        var query = new GetAirportByIdQuery(airportId);

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(airportId))
            .ReturnsAsync((Airport)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airport not found.");
    }
}