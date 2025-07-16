using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;

public record GetAllGendersQuery : IRequest<Result<IEnumerable<GenderDto>>>;
