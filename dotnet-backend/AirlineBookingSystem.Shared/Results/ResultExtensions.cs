using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Shared.Results;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result, 
        string? actionName = null, object? routeValues = null)
    {
        return result.StatusCode switch
        {
            ResultStatusCode.Success => controller.Ok(result),
            ResultStatusCode.Created => HandleCreated(controller, result, actionName, routeValues),
            ResultStatusCode.Accepted => controller.Accepted(result.Value),
            ResultStatusCode.NoContent => controller.NoContent(),
            ResultStatusCode.BadRequest => controller.BadRequest(CreateErrorResponse(result.Error)),
            ResultStatusCode.Unauthorized => controller.StatusCode(401,CreateErrorResponse(result.Error)),
            ResultStatusCode.Forbidden => controller.Forbid(),
            ResultStatusCode.NotFound => controller.NotFound(CreateErrorResponse(result.Error)),
            ResultStatusCode.Conflict => controller.Conflict(CreateErrorResponse(result.Error)),
            ResultStatusCode.UnprocessableEntity => controller.UnprocessableEntity(CreateErrorResponse(result.Error)),
            _ => controller.StatusCode((int)result.StatusCode, CreateErrorResponse(result.Error))
        };
    }

    public static IActionResult ToActionResult(this ControllerBase controller, Result result, 
        string? actionName = null, object? routeValues = null)
    {
        return result.StatusCode switch
        {
            ResultStatusCode.Success => controller.Ok(),
            ResultStatusCode.Created => HandleCreated(controller, result, actionName, routeValues),
            ResultStatusCode.Accepted => controller.Accepted(),
            ResultStatusCode.NoContent => controller.NoContent(),
            ResultStatusCode.BadRequest => controller.BadRequest(CreateErrorResponse(result.Error)),
            ResultStatusCode.Unauthorized => controller.StatusCode(401,CreateErrorResponse(result.Error)),
            ResultStatusCode.Forbidden => controller.Forbid(),
            ResultStatusCode.NotFound => controller.NotFound(CreateErrorResponse(result.Error)),
            ResultStatusCode.Conflict => controller.Conflict(CreateErrorResponse(result.Error)),
            ResultStatusCode.UnprocessableEntity => controller.UnprocessableEntity(CreateErrorResponse(result.Error)),
            _ => controller.StatusCode((int)result.StatusCode, CreateErrorResponse(result.Error))
        };
    }

    private static IActionResult HandleCreated<T>(ControllerBase controller, Result<T> result, 
        string? actionName, object? routeValues)
    {
        if (result.Metadata.TryGetValue("Location", out var location))
        {
            return controller.Created(location.ToString(), result.Value);
        }

        return !string.IsNullOrEmpty(actionName) ? controller.CreatedAtAction(actionName, routeValues, result.Value) : controller.StatusCode(201, result.Value);
    }

    private static IActionResult HandleCreated(ControllerBase controller, Result result, 
        string? actionName, object? routeValues)
    {
        if (result.Metadata.TryGetValue("Location", out var location))
        {
            return controller.Created(location.ToString(), null);
        }
        if (!string.IsNullOrEmpty(actionName))
        {
            return controller.CreatedAtAction(actionName, routeValues, null);
        }

        return controller.StatusCode(201);
    }

    private static object CreateErrorResponse(string error)
    {
        return new { Error = error, Timestamp = DateTime.UtcNow };
    }
}