using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.GetById;

/// <summary>
/// Handles the retrieval of a gate by its unique identifier.
/// </summary>
public class GetGateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetGateByIdQuery, Result<GateDto>>
{
    /// <summary>
    /// Handles the <see cref="GetGateByIdQuery"/> to retrieve a gate by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GateDto}"/> indicating the success or failure of the operation, with the gate DTO on success.</returns>
    public async Task<Result<GateDto>> Handle(GetGateByIdQuery request, CancellationToken cancellationToken)
    {
        var gate = await unitOfWork.Gates.GetByIdAsync(request.Id);
        if (gate == null)
        {
            return Result<GateDto>.Failure("Gate NotFound");
        }
        return Result<GateDto>.Success(mapper.Map<GateDto>(gate));
    }
}
