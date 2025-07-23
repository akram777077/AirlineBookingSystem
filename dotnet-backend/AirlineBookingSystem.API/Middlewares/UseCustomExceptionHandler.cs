using AirlineBookingSystem.Shared.Results.Error;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace AirlineBookingSystem.API.Middlewares;


public static class ExceptionMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                switch (exception)
                {
                    case ValidationException validationEx:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        var validationResponse = new ErrorResultDto
                        {
                            Errors = validationEx.Errors
                                .Select(e => new ErrorItem(e.ErrorMessage))
                                .ToList()
                        };

                        await context.Response.WriteAsJsonAsync(validationResponse);
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        await context.Response.WriteAsJsonAsync(new ErrorResultDto
                        {
                            Message = "An unexpected error occurred",
                            Errors = new List<ErrorItem>()
                        });
                        break;
                }
            });
        });
    }
}