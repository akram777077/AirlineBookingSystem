namespace AirlineBookingSystem.Shared.DTOs.Cities;

public struct CityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CountryId { get; set; }
}
