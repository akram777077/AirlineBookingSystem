using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.BookingStatuses.Queries.GetAll;

public class GetAllBookingStatusesQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllBookingStatusesQueryHandler _handler;

    public GetAllBookingStatusesQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllBookingStatusesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllBookingStatuses()
    {
        // Arrange
        var bookingStatuses = new List<BookingStatus>
        {
            BookingStatusFactory.GetBookingStatusFaker().Generate(),
            BookingStatusFactory.GetBookingStatusFaker().Generate()
        };
        var bookingStatusDtos = new List<BookingStatusDto>
        {
            new BookingStatusDto { Id = bookingStatuses[0].Id, Name = bookingStatuses[0].BookingStatusName.ToString() },
            new BookingStatusDto { Id = bookingStatuses[1].Id, Name = bookingStatuses[1].BookingStatusName.ToString() }
        };

        _unitOfWorkMock.Setup(u => u.BookingStatuses.GetAllAsync()).ReturnsAsync(bookingStatuses);
        _mapperMock.Setup(m => m.Map<IEnumerable<BookingStatusDto>>(bookingStatuses)).Returns(bookingStatusDtos);

        // Act
        var result = await _handler.Handle(new GetAllBookingStatusesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(bookingStatusDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoBookingStatusesExist()
    {
        // Arrange
        var bookingStatuses = new List<BookingStatus>();
        var bookingStatusDtos = new List<BookingStatusDto>();

        _unitOfWorkMock.Setup(u => u.BookingStatuses.GetAllAsync()).ReturnsAsync(bookingStatuses);
        _mapperMock.Setup(m => m.Map<IEnumerable<BookingStatusDto>>(bookingStatuses)).Returns(bookingStatusDtos);

        // Act
        var result = await _handler.Handle(new GetAllBookingStatusesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
