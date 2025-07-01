using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FlightId { get; set; }
    public int BookingStatusId { get; set; }
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public string? TicketNumber { get; set; } // unique
    public required string PaymentStatus { get; set; }
    public int? SeatId { get; set; } // unique
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required User User { get; set; }
    public required Flight Flight { get; set; }
    public required BookingStatus BookingStatus { get; set; }
    public Seat? Seat { get; set; }
}
