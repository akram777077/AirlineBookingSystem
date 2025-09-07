namespace AirlineBookingSystem.API.Routes.BaseRoute;

public abstract class Base
{
    public string BaseRoute { get; }
    protected string GetById { get; }
    public string GetByIdRoute { get; }

    protected Base(string resourceName)
    {
        BaseRoute = $"api/v{{version:apiVersion}}/{resourceName}";
        GetById = "{id:int}";
        GetByIdRoute = $"{BaseRoute}/{GetById}";
    }
}
