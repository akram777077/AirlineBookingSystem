namespace AirlineBookingSystem.Shared.DTOs.airports;

public record AirportSearchResultDto(int Id, string AirportCode, string Name, string CityName, string Timezone);