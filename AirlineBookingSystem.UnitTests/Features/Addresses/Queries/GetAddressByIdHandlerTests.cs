using AirlineBookingSystem.Application.Features.Addresses.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AirlineBookingSystem.UnitTests.Common;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Addresses.Queries;

public class GetAddressByIdHandlerTests
{
    [Fact]
    public async Task Handle_WhenAddressExistsById_ReturnAddress()
    {
        //Arrange
        var addresses = TestData.GetAddressesList();
        var addressDto = TestData.GetAddressDto();
        
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(addresses.FirstOrDefault(a => a.Id == 1));

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<Address>()))
            .Returns((Address address) => addressDto);
        
        var handler = new GetAddressByIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetAddressByIdQuery(1);
        
        //Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal("27000", result.ZipCode);
        Assert.Equal("City Center", result.Street);
    }
    
    [Fact]
    public async Task Handle_WhenAddressDoesNotExistsById_ReturnNull()
    {
        //Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((Address?)null);

        var mapperMock = new Mock<IMapper>();
        var handler = new GetAddressByIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetAddressByIdQuery(-1);
        
        //Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        //Assert
        Assert.Null(result);
    }
}