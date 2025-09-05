namespace AirlineBookingSystem.Shared.Enums;

/// <summary>
/// Represents the status of a flight.
/// </summary>
public enum FlightStatusEnum
{
    /// <summary>
    /// Flight is planned.
    /// </summary>
    Scheduled = 1,
    /// <summary>
    /// Flight is delayed.
    /// </summary>
    Delayed,
    /// <summary>
    /// Flight has been canceled.
    /// </summary>
    Cancelled,
    /// <summary>
    /// Flight has taken off.
    /// </summary>
    Departed,
    /// <summary>
    /// Flight has landed.
    /// </summary>
    Arrived
}
