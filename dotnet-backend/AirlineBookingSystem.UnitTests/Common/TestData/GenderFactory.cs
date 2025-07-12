using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class GenderFactory
{
    public static Faker<Gender> GetGenderFaker()
    {
        return new Faker<Gender>()
            .RuleFor(g => g.Code, f => f.PickRandom<char>('M', 'F', 'O'));
    }
}