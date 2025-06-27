using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class BookingStatusFactory
{
    public static BookingStatus Create(int id = 1, BookingStatusEnum status = BookingStatusEnum.CheckedIn)
    {
        return new BookingStatus
        {
            Id = id,
            StatusName = status
        };
    }
}
