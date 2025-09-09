namespace AirlineBookingSystem.API.Routes;

public static class BookingStatusRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/booking-statuses";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}