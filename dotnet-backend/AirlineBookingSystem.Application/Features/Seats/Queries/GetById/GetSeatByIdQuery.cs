using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

public record GetSeatByIdQuery(int Id) : IRequest<Result<SeatDto>>;