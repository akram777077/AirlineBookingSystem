namespace AirlineBookingSystem.Shared.DTOs.flights;

public class FlightDetailsDto
{
    public class FlightAirplaneDto
    {
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
    public class FlightAirportDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
    }
    public class FlightGateDto
    {
        public string Gate { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public FlightAirportDto Airport { get; set; } = new();
    }
    public class FlightSegmentDto
    {
        public string Gate { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public FlightAirportDto Airport { get; set; } = new();
    }
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public DateTimeOffset DepartureTime { get; set; }
    public DateTimeOffset? ArrivalTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public FlightAirplaneDto Airplane { get; set; } = new();
    public FlightSegmentDto Departure { get; set; } = new();
    public FlightSegmentDto Arrival { get; set; } = new();
    public int TotalBookings { get; set; }
    public int AvailableSeats { get; set; }
}