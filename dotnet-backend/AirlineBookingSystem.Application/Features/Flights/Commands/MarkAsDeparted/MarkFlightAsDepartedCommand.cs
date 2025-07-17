using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsDeparted;

public record MarkFlightAsDepartedCommand(int Id) : IRequest<Result>;
