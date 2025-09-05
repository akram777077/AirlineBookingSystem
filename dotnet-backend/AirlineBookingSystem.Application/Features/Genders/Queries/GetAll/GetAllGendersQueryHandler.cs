using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all genders.
/// </summary>
public class GetAllGendersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllGendersQuery, Result<IEnumerable<GenderDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllGendersQuery"/> to retrieve all genders.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IEnumerable{GenderDto}}"/> containing a list of gender DTOs on success.</returns>
    public async Task<Result<IEnumerable<GenderDto>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
    {
        var genders = await unitOfWork.Genders.GetAllAsync();
        return Result<IEnumerable<GenderDto>>.Success(mapper.Map<IEnumerable<GenderDto>>(genders));
    }
}
