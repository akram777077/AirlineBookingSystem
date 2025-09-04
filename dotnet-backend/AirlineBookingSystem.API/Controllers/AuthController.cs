using AirlineBookingSystem.Application.Features.Auth.Commands.Register;
using AirlineBookingSystem.Application.Features.Auth.Queries.Login;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AirlineBookingSystem.Application.Interfaces;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public AuthController(ISender mediator, ITokenService tokenService, ApplicationDbContext context)
    {
        _mediator = mediator;
        _tokenService = tokenService;
        _context = context;
    }
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return this.ToActionResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        var refreshToken = _tokenService.CreateRefreshToken(result.Value);
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        var accessToken = _tokenService.CreateAccessToken(result.Value);

        // Send refresh token as a secure, http-only cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
            Secure = true, // Set to true in production
            SameSite = SameSiteMode.None // Adjust as needed for your client
        };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

        return Ok(new { AccessToken = accessToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized("Refresh token not found.");
        }

        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User).ThenInclude(u => u.Role)
            .SingleOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedToken == null || !storedToken.IsActive)
        {
            return Unauthorized("Invalid refresh token.");
        }

        var accessToken = _tokenService.CreateAccessToken(storedToken.User);
        return Ok(new { AccessToken = accessToken });
    }

    [Authorize]
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token not found.");
        }

        var storedToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        if (storedToken == null)
        {
            return BadRequest("Invalid refresh token.");
        }

        storedToken.Revoked = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
