namespace AirlineBookingSystem.Shared.Enums;

/// <summary>
/// Represents the roles in the system.
/// </summary>
public enum RoleEnum
{
    /// <summary>
    /// Full access to the system.
    /// </summary>
    Admin,
    /// <summary>
    /// Manages flights, airplanes, and schedules.
    /// </summary>
    AirlineManager,
    /// <summary>
    /// Handles bookings on behalf of customers.
    /// </summary>
    Agent,
    /// <summary>
    /// Regular user who books flights.
    /// </summary>
    Customer,
    /// <summary>
    /// Manages payments and refunds.
    /// </summary>
    FinanceOfficer,
    /// <summary>
    /// Handles passenger check-in.
    /// </summary>
    CheckInStaff,
    /// <summary>
    /// Manages boarding at gates.
    /// </summary>
    GateStaff,
    /// <summary>
    /// Provides customer support.
    /// </summary>
    Support,
    /// <summary>
    /// Read-only access for auditing.
    /// </summary>
    SystemAuditor
}
