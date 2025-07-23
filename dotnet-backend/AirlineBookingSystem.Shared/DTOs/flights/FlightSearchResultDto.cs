namespace AirlineBookingSystem.Shared.DTOs.flights;

public record FlightSearchResultDto(int Id, string FlightNumber, string Airline, string Status, FlightSegmentSearchDto Departure, FlightSegmentSearchDto Arrival);
