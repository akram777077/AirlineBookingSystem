namespace AirlineBookingSystem.Shared.DTOs.Countries;

public class CityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CountryId { get; set; }
}