namespace AirlineBookingSystem.Domain.Entities
{
    public class Person 
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string MidName { get; set; }
        public required string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }
        public char Gender { get; set; }

        public int AddressId { get; set; }
        public required Address Address { get; set; } 
    }
}
