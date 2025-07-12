using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class BookingFactory
{
    public static Faker<Booking> GetBookingFaker(int flightId, int userId, int bookingStatusId)
    {
        return new Faker<Booking>()
            .RuleFor(b => b.BookedAt, f => f.Date.Past())
            .RuleFor(b => b.TicketNumber, f => f.Finance.Account(10))
            .RuleFor(b => b.PaymentStatus, f => f.Finance.TransactionType())
            .RuleFor(b => b.FlightId, flightId)
            .RuleFor(b => b.UserId, userId)
            .RuleFor(b => b.BookingStatusId, bookingStatusId);
    }
}