namespace AirlineBookingSystem.Shared.DTOs.Countries;

public class CountryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
}