namespace AirlineBookingSystem.Shared.DTOs.Addresses;

public class AddressDto
{
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }
    public int CityId { get; set; }
    public int CountryId { get; set; }
}