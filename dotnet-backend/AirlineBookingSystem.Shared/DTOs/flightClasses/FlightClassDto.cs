namespace AirlineBookingSystem.Shared.DTOs.flightClasses;

public record FlightClassDto(int Id, int FlightId, int ClassTypeId, decimal Price, int TotalSeats, int AvailableSeats);
