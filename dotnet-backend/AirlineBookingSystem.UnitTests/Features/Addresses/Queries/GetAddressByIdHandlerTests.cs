using AirlineBookingSystem.Application.Features.Addresses.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AirlineBookingSystem.UnitTests.Common;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Addresses.Queries;

public class GetAddressByIdHandlerTests
{
    [Fact]
    public async Task Handle_WhenAddressExistsById_ReturnAddress()
    {
        //Arrange
        var address = AddressFactory.Create(1, "City Center", "27000");
        var addressDto = new AddressDto
        {
            Id = address.Id,
            Street = address.Street,
            ZipCode = address.ZipCode,
            CityId = address.CityId
        };
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(address);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AddressDto>(address))
            .Returns(addressDto);
        
        var handler = new GetAddressByIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetAddressByIdQuery(1);
        
        //Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        //Assert
        result.Should().BeEquivalentTo(addressDto);
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
        result.Should().BeNull();
    }
}