namespace AirlineBookingSystem.Shared.DTOs.Genders;

/// <summary>
/// Represents a gender data transfer object.
/// </summary>
public struct GenderDto
{
    /// <summary>
    /// Gets or sets the ID of the gender.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the code of the gender.
    /// </summary>
    public required string Code { get; set; }
}
