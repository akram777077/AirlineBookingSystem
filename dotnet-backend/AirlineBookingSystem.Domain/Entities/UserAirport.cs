namespace AirlineBookingSystem.Domain.Entities;

public class UserAirport
{
    public int UserId { get; set; }
    public int AirportId { get; set; }
    public required User User { get; set; }
    public required Airport Airport { get; set; }
}
