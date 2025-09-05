using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all class types.
/// </summary>
public class GetAllClassTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllClassTypesQuery, Result<IEnumerable<ClassTypeDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllClassTypesQuery"/> to retrieve all class types.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IEnumerable{ClassTypeDto}}"/> containing a list of class type DTOs on success.</returns>
    public async Task<Result<IEnumerable<ClassTypeDto>>> Handle(GetAllClassTypesQuery request, CancellationToken cancellationToken)
    {
        var classTypes = await unitOfWork.ClassTypes.GetAllAsync();
        return Result<IEnumerable<ClassTypeDto>>.Success(mapper.Map<IEnumerable<ClassTypeDto>>(classTypes));
    }
}
