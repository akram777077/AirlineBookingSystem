namespace AirlineBookingSystem.Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
     
        public int PersonId { get; set; }
        public required Person Person { get; set; }
                
        public int RoleId { get; set; }
        public required Role Role { get; set; }
      
    }
}
