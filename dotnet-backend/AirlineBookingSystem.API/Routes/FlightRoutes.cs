namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class FlightRoutes : Base
{
    public FlightRoutes() : base("flights") { }
    public const string Search = "search";
    public const string MarkAsDeparted = "{id:int}/mark-departed";
    public const string MarkAsArrived = "{id:int}/mark-arrived";
}