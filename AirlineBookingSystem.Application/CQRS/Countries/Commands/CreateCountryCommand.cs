using MediatR;

namespace AirlineBookingSystem.Application.CQRS.Countries.Commands;

public class CreateCountryCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
}