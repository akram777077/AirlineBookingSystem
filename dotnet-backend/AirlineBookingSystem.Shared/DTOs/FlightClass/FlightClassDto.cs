namespace AirlineBookingSystem.Shared.DTOs.FlightClass;

public record FlightClassDto(int Id, int FlightId, int ClassId, decimal Price, int Seats);
