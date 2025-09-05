using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.Search;

/// <summary>
/// Handles the search for gates based on a filter.
/// </summary>
public class SearchGatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchGatesQuery, PagedResult<List<GateDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchGatesQuery"/> to search for gates.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{GateDto}}"/> containing a paginated list of gate DTOs.</returns>
    public async Task<PagedResult<List<GateDto>>> Handle(SearchGatesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Gate> query = unitOfWork.Gates.GetAll().Include(g => g.Terminal);

        if (!string.IsNullOrWhiteSpace(request.Filter.GateNumber))
        {
            query = query.Where(g => g.GateNumber.Contains(request.Filter.GateNumber));
        }

        if (request.Filter.TerminalId.HasValue)
        {
            query = query.Where(g => g.TerminalId == request.Filter.TerminalId.Value);
        }

        var pagedResult = await PagedResult<GateDto>.ToPagedList(
            query.Select(g => mapper.Map<GateDto>(g)),
            request.Filter.PageNumber,
            request.Filter.PageSize);

        return pagedResult;
    }
}
