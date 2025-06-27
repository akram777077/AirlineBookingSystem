using AirlineBookingSystem.Application.Features.Airports.Queries.ByCode;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries;

public class GetAirportByCodeHandlerTests
{
    [Fact]
    public async Task GetAirportByCode_ShouldReturnAirport_WhenExists()
    {
       var mockAirportRepository = new Mock<IAirportRepository>();
       var mockMapper = new Mock<IMapper>();
       
       var airportEntity = AirportFactory.Create();
       var airportDto = airportEntity.ToDto();
    
       mockAirportRepository
           .Setup(repo => repo.GetByCodeAsync(airportEntity.AirportCode))
           .ReturnsAsync(airportEntity);
       mockMapper
           .Setup(m => m.Map<AirportDto>(It.IsAny<Airport>()))
           .Returns(airportDto);
       var handler = new GetAirportByCodeHandler(mockAirportRepository.Object, mockMapper.Object);
       var query = new GetAirportByCodeQuery(airportEntity.AirportCode);
         // Act
         var result = await handler.Handle(query, CancellationToken.None);
         // Assert
         result.Should().BeEquivalentTo(airportDto);
    }
    [Fact]
    public async Task GetAirportByCode_ShouldReturnNull_WhenDoesNotExist()
    {
        // Arrange
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        string code = "XYZ"; // Non-existent code
        mockAirportRepository
            .Setup(repo => repo.GetByCodeAsync(code))
            .ReturnsAsync((Airport?)null);
        var handler = new GetAirportByCodeHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportByCodeQuery(code);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
        mockAirportRepository.Verify(repo => repo.GetByCodeAsync(code), Times.Once);
    }
}