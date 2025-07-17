using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.GetById;

public class GetAirportByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAirportByIdQuery, Result<AirportDto>>
{
    public async Task<Result<AirportDto>> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
    {
        var airport = await unitOfWork.Airports.GetByIdAsync(request.Id);
        return airport == null ? Result<AirportDto>.NotFound("Airport not found.") : Result<AirportDto>.Success(mapper.Map<AirportDto>(airport));
    }
}