using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.BookingStatuses.Queries;

public class GetAllBookingStatusesHandlerTests
{
    [Fact]
    public async Task GetAllBookingStatusesHandler_ShouldReturnAllBookingStatuses()
    {
        var mockRepository = new Mock<IBookingStatusRepository>();
        var mockMapper = new Mock<IMapper>();
        var bookingStatuses = new List<BookingStatus>
        { 
            BookingStatusFactory.Create(),
            BookingStatusFactory.Create(2, BookingStatusEnum.Cancelled)
        };
        var bookingStatusesDto = new List<BookingStatusDto>
        {
            new BookingStatusDto { Id = bookingStatuses[0].Id, StatusName = bookingStatuses[0].StatusName.ToString() },
            new BookingStatusDto { Id = bookingStatuses[1].Id, StatusName = bookingStatuses[1].StatusName.ToString() }
        };
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookingStatuses);
        mockMapper.Setup(m => m.Map<List<BookingStatusDto>>(bookingStatuses)).Returns(bookingStatusesDto);
        // Arrange
        var handler = new GetAllBookingStatusesHandler(mockRepository.Object, mockMapper.Object);
        
        // Act
        var result = await handler.Handle(new GetAllBookingStatusesQuery(), CancellationToken.None);
        
        // Assert
        var resultList = result.ToList();
        for (int i = 0; i < bookingStatusesDto.Count; i++)
        {
            resultList[i].Should().BeEquivalentTo(bookingStatusesDto[i]);
        }
    }
}