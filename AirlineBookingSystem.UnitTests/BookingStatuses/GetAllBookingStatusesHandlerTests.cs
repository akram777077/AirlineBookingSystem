using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AirlineBookingSystem.Shared.Enums;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.BookingStatuses;

public class GetAllBookingStatusesHandlerTests
{
    [Fact]
    public async Task GetAllBookingStatusesHandler_ShouldReturnAllBookingStatuses()
    {
        var mockRepository = new Mock<IBookingStatusRepository>();
        var mockMapper = new Mock<IMapper>();
        var bookingStatusesDto = new List<BookingStatusDto>
        {
            new BookingStatusDto { Id = 1, StatusName = "CheckedIn" },
            new BookingStatusDto { Id = 2, StatusName = "Cancelled" }
        };
        var bookingStatuses = new List<BookingStatus>
        {
            new BookingStatus { Id = 1, StatusName = BookingStatusEnum.CheckedIn },
            new BookingStatus { Id = 2, StatusName = BookingStatusEnum.Cancelled },
        };
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookingStatuses);
        mockMapper.Setup(m => m.Map<List<BookingStatusDto>>(bookingStatuses)).Returns(bookingStatusesDto);
        // Arrange
        var handler = new GetAllBookingStatusesHandler(mockRepository.Object, mockMapper.Object);
        
        // Act
        var result = await handler.Handle(new GetAllBookingStatusesQuery(), CancellationToken.None);
        
        // Assert
        var resultList = result.ToList();
        Assert.NotNull(resultList);
        Assert.NotEmpty(resultList);
        Assert.All(resultList, status => Assert.NotNull(status.StatusName));
        Assert.Equal(2, resultList.Count());
        Assert.Equal("CheckedIn", resultList[0].StatusName);
        Assert.Equal("Cancelled", resultList[1].StatusName);
    }
}