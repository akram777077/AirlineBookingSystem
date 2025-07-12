using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class PaymentFactory
{
    public static Faker<Payment> GetPaymentFaker(int bookingId)
    {
        return new Faker<Payment>()
            .RuleFor(p => p.Amount, f => f.Random.Decimal(50, 2000))
            .RuleFor(p => p.PaymentDate, f => f.Date.Past())
            .RuleFor(p => p.BookingId, bookingId);
    }
}