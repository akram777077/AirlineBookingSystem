using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

public class GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryByIdQuery, CountryDto>
{
    public async Task<CountryDto> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await unitOfWork.Countries.GetByIdAsync(request.Id);
        return mapper.Map<CountryDto>(country);
    }
}
