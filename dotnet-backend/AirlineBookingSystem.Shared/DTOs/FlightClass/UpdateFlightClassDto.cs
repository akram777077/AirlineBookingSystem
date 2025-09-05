namespace AirlineBookingSystem.Shared.DTOs.FlightClass;

/// <summary>
/// Represents a data transfer object for updating a flight class.
/// </summary>
/// <param name="Id">The ID of the flight class.</param>
/// <param name="Price">The price of the flight class.</param>
/// <param name="Seats">The number of seats in the flight class.</param>
public record UpdateFlightClassDto(int Id, decimal Price, int Seats);