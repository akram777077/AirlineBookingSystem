namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class FlightClassRoutes : Base
{
    public FlightClassRoutes() : base("flight-classes") { }
    public const string GetByFlightId = "by-flight/{flightId:int}";
}