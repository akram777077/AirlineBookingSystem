using AirlineBookingSystem.Shared.Results.Error;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace AirlineBookingSystem.API.Middlewares;


/// <summary>
/// Extension methods for setting up custom exception handling middleware.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Adds a custom exception handler to the application's request pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
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
                                .Select(e => new ErrorResultDto.ErrorItem
                                {
                                    Error = e.ErrorMessage
                                }).ToList()
                        };

                        await context.Response.WriteAsJsonAsync(validationResponse);
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        await context.Response.WriteAsJsonAsync(new ErrorResultDto
                        {
                            Message = "An unexpected error occurred",
                            Errors = new List<ErrorResultDto.ErrorItem>()
                        });
                        break;
                }
            });
        });
    }
}