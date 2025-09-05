
namespace AirlineBookingSystem.Shared.Enums;

/// <summary>
/// Represents the status of a booking.
/// </summary>
public enum BookingStatusEnum
{
    /// <summary>
    /// Booking is confirmed.
    /// </summary>
    Booked,      
    /// <summary>
    /// Passenger has checked in.
    /// </summary>
    CheckedIn,   
    /// <summary>
    /// Booking was canceled.
    /// </summary>
    Cancelled    
}