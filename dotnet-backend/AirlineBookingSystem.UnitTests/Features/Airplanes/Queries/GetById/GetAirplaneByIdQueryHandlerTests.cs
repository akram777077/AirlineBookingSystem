using AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;
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

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Queries.GetById;

public class GetAirplaneByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAirplaneByIdQueryHandler _handler;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;

    public GetAirplaneByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
        _handler = new GetAirplaneByIdQueryHandler(_airplaneRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAirplaneDto_WhenAirplaneExists()
    {
        // Arrange
        var airplaneId = 1;
        var query = new GetAirplaneByIdQuery(airplaneId);
        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        airplane.Id = airplaneId;
        var airplaneDto = new AirplaneDto { Model = "Test Model", Manufacturer = "Test Manufacturer", Capacity = 100, Code = "ABC" };

        _airplaneRepositoryMock.Setup(r => r.GetByIdAsync(airplaneId)).ReturnsAsync(airplane);
        _mapperMock.Setup(m => m.Map<AirplaneDto>(airplane)).Returns(airplaneDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(airplaneDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAirplaneDoesNotExist()
    {
        // Arrange
        var airplaneId = 1;
        var query = new GetAirplaneByIdQuery(airplaneId);

        _airplaneRepositoryMock.Setup(r => r.GetByIdAsync(airplaneId)).ReturnsAsync((Airplane)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airplane not found.");
    }
}