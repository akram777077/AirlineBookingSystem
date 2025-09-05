namespace AirlineBookingSystem.Shared.DTOs.Cities;

/// <summary>
/// Represents a city data transfer object.
/// </summary>
public struct CityDto
{
    /// <summary>
    /// Gets or sets the ID of the city.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Gets or sets the ID of the country where the city is located.
    /// </summary>
    public int CountryId { get; set; }
}
