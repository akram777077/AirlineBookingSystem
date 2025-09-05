
namespace AirlineBookingSystem.Shared.DTOs.countries;

/// <summary>
/// Represents a country data transfer object.
/// </summary>
public struct CountryDto
{
    /// <summary>
    /// Gets or sets the ID of the country.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Gets or sets the code of the country.
    /// </summary>
    public required string Code { get; set; }
}
