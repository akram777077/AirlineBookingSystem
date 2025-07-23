namespace AirlineBookingSystem.Shared.DTOs.flights;

public record FlightDetailsDto(int Id, string FlightNumber, DateTimeOffset DepartureTime, DateTimeOffset? ArrivalTime, DateTime CreatedAt, DateTime? UpdatedAt, string Status, FlightAirplaneDto Airplane, FlightSegmentDto Departure, FlightSegmentDto Arrival, int TotalBookings, int AvailableSeats);