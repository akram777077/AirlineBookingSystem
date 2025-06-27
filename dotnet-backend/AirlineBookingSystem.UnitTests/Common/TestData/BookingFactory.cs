using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class BookingFactory
{
    public static Booking Create(int id = 1, string seat = "12A")
    {
        return new Booking
        {
            Id = id,
            SeatNumber = seat,
            PassengerId = 1,
            Passenger = UserFactory.Create(),
            FlightId = 1,
            Flight = FlightFactory.Create(),
            BookingStatusId = 1,
            BookingStatus = BookingStatusFactory.Create()
        };
    }
}
