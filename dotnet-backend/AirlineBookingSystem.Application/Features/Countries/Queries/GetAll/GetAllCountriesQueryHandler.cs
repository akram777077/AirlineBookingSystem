using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, Result<List<CountryDto>>>
{
    public async Task<Result<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await unitOfWork.Countries.GetAllAsync();
        return Result<List<CountryDto>>.Success(mapper.Map<List<CountryDto>>(countries));
    }
}
