namespace AirlineBookingSystem.Shared.DTOs.airports;

public record AirportDto(int Id, string AirportCode, string Name, int CityId, string Timezone);