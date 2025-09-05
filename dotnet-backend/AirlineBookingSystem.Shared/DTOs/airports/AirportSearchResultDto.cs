namespace AirlineBookingSystem.Shared.DTOs.airports;

/// <summary>
/// Represents an airport search result data transfer object.
/// </summary>
public struct AirportSearchResultDto
{
    /// <summary>
    /// Gets or sets the ID of the airport.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the code of the airport.
    /// </summary>
    public string AirportCode { get; set; }
    /// <summary>
    /// Gets or sets the name of the airport.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the name of the city where the airport is located.
    /// </summary>
    public string CityName { get; set; }
    /// <summary>
    /// Gets or sets the timezone of the airport.
    /// </summary>
    public string Timezone { get; set; }
}