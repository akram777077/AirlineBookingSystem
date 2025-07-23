namespace AirlineBookingSystem.Shared.DTOs.airports;

public record UpdateAirportDto(int Id, string AirportCode, string Name, int CityId, string Timezone);