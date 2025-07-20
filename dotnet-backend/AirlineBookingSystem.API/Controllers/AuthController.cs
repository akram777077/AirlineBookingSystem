using AirlineBookingSystem.Application.Features.Auth.Commands.Register;
using AirlineBookingSystem.Application.Features.Auth.Queries.Login;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
