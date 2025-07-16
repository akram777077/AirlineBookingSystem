namespace AirlineBookingSystem.Shared.DTOs.Cities;

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }
}
