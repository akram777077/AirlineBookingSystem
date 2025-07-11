using AirlineBookingSystem.Domain.Entities;
using Bogus;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class BookingStatusFactory
{
    public static Faker<BookingStatus> GetBookingStatusFaker()
    {
        return new Faker<BookingStatus>()
            .RuleFor(bs => bs.BookingStatusName, f => f.PickRandom<BookingStatusEnum>());
    }
}