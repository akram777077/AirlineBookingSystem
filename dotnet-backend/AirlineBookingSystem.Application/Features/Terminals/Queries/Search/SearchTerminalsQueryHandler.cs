using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

public class SearchTerminalsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchTerminalsQuery, PagedResult<List<TerminalDto>>>
{
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