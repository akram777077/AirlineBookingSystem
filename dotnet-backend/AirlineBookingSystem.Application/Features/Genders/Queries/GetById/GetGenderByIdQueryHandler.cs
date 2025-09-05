using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

/// <summary>
/// Handles the retrieval of a gender by its unique identifier.
/// </summary>
public class GetGenderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetGenderByIdQuery, Result<GenderDto>>
{
    /// <summary>
    /// Handles the <see cref="GetGenderByIdQuery"/> to retrieve a gender by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GenderDto}"/> indicating the success or failure of the operation, with the gender DTO on success.</returns>
    public async Task<Result<GenderDto>> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
    {
        var gender = await unitOfWork.Genders.GetByIdAsync(request.Id);
        return gender == null ? Result<GenderDto>.NotFound("Gender not found.") : Result<GenderDto>.Success(mapper.Map<GenderDto>(gender));
    }
}
