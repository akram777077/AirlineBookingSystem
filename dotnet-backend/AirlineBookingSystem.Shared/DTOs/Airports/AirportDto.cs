namespace AirlineBookingSystem.Shared.DTOs.Airports;

public class AirportDto
{
    public int Id { get; set; }
    public required string AirportCode { get; set; }
    public required string Name { get; set; }
    public int CityId { get; set; }
}