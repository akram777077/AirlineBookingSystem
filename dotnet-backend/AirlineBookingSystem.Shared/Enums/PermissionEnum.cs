namespace AirlineBookingSystem.Shared.Enums;

/// <summary>
/// Represents the permissions in the system.
/// </summary>
public enum PermissionEnum
{
    // General User Management
    CreateUser,
    ViewUser,
    EditUser,
    DeleteUser,

    // Flights
    CreateFlight,
    ViewFlight,
    EditFlight,
    DeleteFlight,

    // Bookings
    CreateBooking,
    ViewBooking,
    EditBooking,
    CancelBooking,

    // Payments
    ViewPayments,
    ProcessRefunds,

    // Airports & Gates
    ManageAirport,
    ManageGate,

    // Role & Permission Management
    AssignRoles,
    AssignPermissions
}

