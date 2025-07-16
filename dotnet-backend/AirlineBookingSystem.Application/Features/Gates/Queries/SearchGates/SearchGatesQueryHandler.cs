using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.SearchGates;

public class SearchGatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchGatesQuery, PagedResult<List<GateDto>>>
{
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
