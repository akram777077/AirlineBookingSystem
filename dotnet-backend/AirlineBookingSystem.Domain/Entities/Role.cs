using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public UserRoleEnum RoleName { get; set; } 
    }
}
