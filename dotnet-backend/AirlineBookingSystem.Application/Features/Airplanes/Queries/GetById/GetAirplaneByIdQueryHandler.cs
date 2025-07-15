using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

public class GetAirplaneByIdQueryHandler(IAirplaneRepository airplaneRepository, IMapper mapper)
    : IRequestHandler<GetAirplaneByIdQuery, Result<AirplaneDto>>
{
    public async Task<Result<AirplaneDto>> Handle(GetAirplaneByIdQuery request, CancellationToken cancellationToken)
    {
        var airplane = await airplaneRepository.GetByIdAsync(request.Id);
        if (airplane == null)
        {
            return Result<AirplaneDto>.NotFound("Airplane not found.");
        }
        return Result<AirplaneDto>.Success(mapper.Map<AirplaneDto>(airplane));
    }
}