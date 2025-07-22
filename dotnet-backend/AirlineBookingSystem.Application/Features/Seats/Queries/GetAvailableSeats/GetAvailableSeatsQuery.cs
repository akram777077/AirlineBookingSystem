using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

public record GetAvailableSeatsQuery(GetAvailableSeatsFilterDto Filter) : IRequest<Result<List<SeatDto>>>;