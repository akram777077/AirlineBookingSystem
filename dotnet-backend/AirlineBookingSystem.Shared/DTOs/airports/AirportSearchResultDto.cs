namespace AirlineBookingSystem.Shared.DTOs.airports;

public struct AirportSearchResultDto
{
    public int Id { get; set; }
    public string AirportCode { get; set; }
    public string Name { get; set; }
    public string CityName { get; set; }
    public string Timezone { get; set; }
}