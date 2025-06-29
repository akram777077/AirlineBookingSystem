using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Gate
{
    public int Id { get; set; }
    public required string GateNumber { get; set; }
    public int TerminalId { get; set; }
    public required Terminal Terminal { get; set; }
    public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
    public ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
}
