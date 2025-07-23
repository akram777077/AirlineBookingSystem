namespace AirlineBookingSystem.Shared.DTOs.Seats;

public record GetAvailableSeatsFilterDto(int FlightId, int? ClassTypeId);