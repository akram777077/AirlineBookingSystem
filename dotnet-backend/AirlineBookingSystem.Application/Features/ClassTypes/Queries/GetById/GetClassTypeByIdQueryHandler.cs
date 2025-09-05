using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

/// <summary>
/// Handles the retrieval of a class type by its unique identifier.
/// </summary>
public class GetClassTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetClassTypeByIdQuery, Result<ClassTypeDto>>
{
    /// <summary>
    /// Handles the <see cref="GetClassTypeByIdQuery"/> to retrieve a class type by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{ClassTypeDto}"/> indicating the success or failure of the operation, with the class type DTO on success.</returns>
    public async Task<Result<ClassTypeDto>> Handle(GetClassTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var classType = await unitOfWork.ClassTypes.GetByIdAsync(request.Id);
        return classType == null ? Result<ClassTypeDto>.NotFound("Class type not found.") : Result<ClassTypeDto>.Success(mapper.Map<ClassTypeDto>(classType));
    }
}
