namespace AirlineBookingSystem.Shared.DTOs.airports;

public record CreateAirportDto(string AirportCode, string Name, int CityId, string Timezone);