
namespace AirlineBookingSystem.Shared.DTOs.FlightClass;

/// <summary>
/// Represents a data transfer object for creating a flight class.
/// </summary>
/// <param name="FlightId">The ID of the flight.</param>
/// <param name="ClassId">The ID of the class type.</param>
/// <param name="Price">The price of the flight class.</param>
/// <param name="Seats">The number of seats in the flight class.</param>
public record CreateFlightClassDto(int FlightId, int ClassId, decimal Price, int Seats);
