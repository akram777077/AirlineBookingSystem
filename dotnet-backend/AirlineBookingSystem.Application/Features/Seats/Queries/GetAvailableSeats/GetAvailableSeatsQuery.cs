using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

/// <summary>
/// Represents a query to retrieve available seats based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for retrieving available seats.</param>
public record GetAvailableSeatsQuery(GetAvailableSeatsFilterDto Filter) : IRequest<Result<List<SeatDto>>>;