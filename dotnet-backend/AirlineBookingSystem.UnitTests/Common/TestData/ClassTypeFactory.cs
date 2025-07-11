using AirlineBookingSystem.Domain.Entities;
using Bogus;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class ClassTypeFactory
{
    public static Faker<ClassType> GetClassTypeFaker()
    {
        return new Faker<ClassType>()
            .RuleFor(ct => ct.Name, f => f.PickRandom<ClassTypeEnum>());
    }
}