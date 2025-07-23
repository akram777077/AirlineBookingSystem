namespace AirlineBookingSystem.Shared.DTOs.flights;

public record FlightSegmentSearchDto(string City, string Country, string AirportCode, DateTimeOffset? Time);