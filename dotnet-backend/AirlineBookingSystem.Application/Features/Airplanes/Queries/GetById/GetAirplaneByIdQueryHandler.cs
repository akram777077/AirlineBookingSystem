using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

/// <summary>
/// Represents a query handler for getting an airplane by its ID.
/// </summary>
public class GetAirplaneByIdQueryHandler: IRequestHandler<GetAirplaneByIdQuery,Result<AirplaneDto>>
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAirplaneByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="airplaneRepository">The airplane repository.</param>
    /// <param name="mapper">The mapper.</param>
    public GetAirplaneByIdQueryHandler(IAirplaneRepository airplaneRepository, IMapper mapper)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the get airplane by ID query.
    /// </summary>
    /// <param name="request">The get airplane by ID query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The airplane DTO.</returns>
    public async Task<Result<AirplaneDto>> Handle(GetAirplaneByIdQuery request, CancellationToken cancellationToken)
    {
        var airplane = await _airplaneRepository.GetByIdAsync(request.Id);
        if (airplane == null)
            return Result.Failure<AirplaneDto>($"Airplane with ID {request.Id} not found.", ResultStatusCode.NotFound);
        return Result<AirplaneDto>.Success(_mapper.Map<AirplaneDto>(airplane));
    }
}