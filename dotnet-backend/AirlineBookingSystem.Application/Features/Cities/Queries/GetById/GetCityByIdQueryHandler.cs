using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

public class GetCityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCityByIdQuery, CityDto>
{
    public async Task<CityDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await unitOfWork.Cities.GetByIdAsync(request.Id);
        return mapper.Map<CityDto>(city);
    }
}
