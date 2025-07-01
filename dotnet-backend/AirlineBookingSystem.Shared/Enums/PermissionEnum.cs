namespace AirlineBookingSystem.Shared.Enums;

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

