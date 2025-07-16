using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryDto>>
{
    public async Task<IEnumerable<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await unitOfWork.Countries.GetAllAsync();
        return mapper.Map<IEnumerable<CountryDto>>(countries);
    }
}
