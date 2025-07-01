namespace AirlineBookingSystem.Shared.Enums;

public enum RoleEnum
{
    Admin,             // Full access to the system
    AirlineManager,    // Manages flights, airplanes, and schedules
    Agent,             // Handles bookings on behalf of customers
    Customer,          // Regular user who books flights
    FinanceOfficer,    // Manages payments and refunds
    CheckInStaff,      // Handles passenger check-in
    GateStaff,         // Manages boarding at gates
    Support,           // Provides customer support
    SystemAuditor      // Read-only access for auditing
}
