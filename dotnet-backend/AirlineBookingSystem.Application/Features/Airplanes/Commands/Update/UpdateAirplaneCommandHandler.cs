using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork; // Added this using statement

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

/// <summary>
/// Represents a command handler for updating an airplane.
/// </summary>
using AirlineBookingSystem.Shared.Results;
using MediatR;

public class UpdateAirplaneCommandHandler : IRequestHandler<UpdateAirplaneCommand, Result<Unit>>
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAirplaneCommandHandler"/> class.
    /// </summary>
    /// <param name="airplaneRepository">The airplane repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public UpdateAirplaneCommandHandler(IAirplaneRepository airplaneRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the update airplane command.
    /// </summary>
    /// <param name="request">The update airplane command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task<Result<Unit>> Handle(UpdateAirplaneCommand request, CancellationToken cancellationToken)
    {
        var airplane = await _airplaneRepository.GetByIdAsync(request.Id);
        if (airplane == null)
            return Result.Failure<Unit>("Airplane not found.", ResultStatusCode.NotFound);

        _mapper.Map(request, airplane);
        _airplaneRepository.Update(airplane);
        await _unitOfWork.CompleteAsync();
        return Result<Unit>.Success(Unit.Value);
    }
}