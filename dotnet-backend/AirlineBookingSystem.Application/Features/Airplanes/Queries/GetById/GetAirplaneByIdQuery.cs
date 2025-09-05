using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

/// <summary>
/// Represents a query to get an airplane by its ID.
/// </summary>
public class GetAirplaneByIdQuery : IRequest<AirplaneDto>
{
    /// <summary>
    /// Gets or sets the ID of the airplane.
    /// </summary>
    public int Id { get; set; }
}