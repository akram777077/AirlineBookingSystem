using System;

namespace AirlineBookingSystem.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public required string PaymentMethod { get; set; }
    public required string TransactionId { get; set; }
    public DateTime PaymentDate { get; set; }
    public required Booking Booking { get; set; }
}
