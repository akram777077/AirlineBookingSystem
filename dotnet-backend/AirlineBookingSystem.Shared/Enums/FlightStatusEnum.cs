namespace AirlineBookingSystem.Shared.Enums;

public enum FlightStatusEnum
{
    Scheduled = 1,   // Flight is planned
    Delayed,     // Flight is delayed
    Cancelled,   // Flight has been canceled
    Departed,    // Flight has taken off
    Arrived      // Flight has landed
}
