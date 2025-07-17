using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

public class GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryByIdQuery, Result<CountryDto>>
{
    public async Task<Result<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await unitOfWork.Countries.GetByIdAsync(request.Id);
        return country == null ? Result<CountryDto>.NotFound("Country not found") : Result<CountryDto>.Success(mapper.Map<CountryDto>(country));
    }
}
