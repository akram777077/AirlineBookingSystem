using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents a booking status entity.
    /// </summary>
    public class BookingStatus
    {
        /// <summary>
        /// Gets or sets the ID of the booking status.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the booking status.
        /// </summary>
        public BookingStatusEnum BookingStatusName { get; set; }
        
    }
}
