using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork; // Added this using statement

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

/// <summary>
/// Represents a command handler for creating a new airplane.
/// </summary>
using AirlineBookingSystem.Shared.Results;

public class CreateAirplaneCommandHandler : IRequestHandler<CreateAirplaneCommand, Result<AirplaneDto>>
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAirplaneCommandHandler"/> class.
    /// </summary>
    /// <param name="airplaneRepository">The airplane repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public CreateAirplaneCommandHandler(IAirplaneRepository airplaneRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the create airplane command.
    /// </summary>
    /// <param name="request">The create airplane command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created airplane DTO.</returns>
    public async Task<Result<AirplaneDto>> Handle(CreateAirplaneCommand request, CancellationToken cancellationToken)
    {
    var airplane = _mapper.Map<Airplane>(request);
    await _airplaneRepository.AddAsync(airplane);
    await _unitOfWork.CompleteAsync();
    var dto = _mapper.Map<AirplaneDto>(airplane);
    return Result<AirplaneDto>.Success(dto);
    }
}