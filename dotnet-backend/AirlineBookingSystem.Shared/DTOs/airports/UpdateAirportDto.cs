namespace AirlineBookingSystem.Shared.DTOs.airports;

public class UpdateAirportDto
{
    public int Id { get; set; }
    public string AirportCode { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
    public string Timezone { get; set; }
}