using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using MediatR;

namespace AirlineBookingSystem.Application.CQRS.Countries.Commands.Create;

public class CreateCountryHandler(ICountryRepository repository) : IRequestHandler<CreateCountryCommand, int>
{
    private readonly ICountryRepository _repository = repository;

    public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new Country
        {
            Name = request.Name,
            Code = request.Code
        };
        await _repository.AddAsync(country);
        return country.Id;
    }
}