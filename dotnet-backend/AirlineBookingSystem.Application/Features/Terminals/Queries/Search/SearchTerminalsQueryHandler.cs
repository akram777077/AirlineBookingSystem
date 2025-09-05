using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

/// <summary>
/// Handles the search for terminals based on a filter.
/// </summary>
public class SearchTerminalsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchTerminalsQuery, PagedResult<List<TerminalDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchTerminalsQuery"/> to search for terminals.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{TerminalDto}}"/> containing a paginated list of terminal DTOs.</returns>
    public async Task<PagedResult<List<TerminalDto>>> Handle(SearchTerminalsQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Terminals.GetAll();

        if (request.Filter.AirportId.HasValue)
        {
            query = query.Where(t => t.AirportId == request.Filter.AirportId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Filter.Name))
        {
            query = query.Where(t => t.Name.Contains(request.Filter.Name));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var terminals = await query
            .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .ToListAsync(cancellationToken);

        var terminalDtos = mapper.Map<List<TerminalDto>>(terminals);

        return new PagedResult<List<TerminalDto>>(terminalDtos, request.Filter.PageNumber, request.Filter.PageSize, totalCount);
    }
}
