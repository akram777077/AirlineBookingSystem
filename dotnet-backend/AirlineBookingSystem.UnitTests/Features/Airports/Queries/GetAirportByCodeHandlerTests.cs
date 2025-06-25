using AirlineBookingSystem.Application.Features.Airports.Queries.ByCode;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries;

public class GetAirportByCodeHandlerTests
{
    [Fact]
    public async Task GetAirportByCode_ShouldReturnAirport_WhenExists()
    {
       var mockAirportRepository = new Mock<IAirportRepository>();
       var mockMapper = new Mock<IMapper>();
       string code = "ORN";
       var airportEntity = new Airport
       {
           Id = 1,
           Name = "Oran Airport",
           AirportCode = code,
           CityId = 1,
           City = new City 
           { Id = 1, Name = "Oran", CountryId = 1,
               Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" } 
           },
           CountryId = 1,
           Country = new Country
           {
               Id = 1, Name = "Algeria", Code = "DZ"
           }
       };
       var airportDto = new AirportDto
       {
           Id = 1,
           Name = "Oran Airport",
           AirportCode = code,
           CityId = 1,
           CountryId = 1
       };
       mockAirportRepository
           .Setup(repo => repo.GetByCodeAsync(code))
           .ReturnsAsync(airportEntity);
       mockMapper
           .Setup(m => m.Map<AirportDto>(It.IsAny<Airport>()))
           .Returns(airportDto);
       var handler = new GetAirportByCodeHandler(mockAirportRepository.Object, mockMapper.Object);
       var query = new GetAirportByCodeQuery(code);
         // Act
         var result = await handler.Handle(query, CancellationToken.None);
         // Assert
         Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Oran Airport", result.Name);
            Assert.Equal("ORN", result.AirportCode);
            Assert.Equal(1, result.CityId);
            Assert.Equal(1, result.CountryId);
         mockAirportRepository.Verify(repo => repo.GetByCodeAsync(code), Times.Once);
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
        Assert.Null(result);
        mockAirportRepository.Verify(repo => repo.GetByCodeAsync(code), Times.Once);
    }
}