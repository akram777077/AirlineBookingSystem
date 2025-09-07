using AirlineBookingSystem.Application.Features.Auth.Commands.Register;
using AirlineBookingSystem.Application.Features.Auth.Queries.Login;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AirlineBookingSystem.Application.Interfaces;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for user authentication and authorization operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[EnableRateLimiting("fixed")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR sender for dispatching commands and queries.</param>
    /// <param name="tokenService">The service for handling JWT and refresh token operations.</param>
    /// <param name="context">The application database context.</param>
    public AuthController(ISender mediator, ITokenService tokenService, ApplicationDbContext context)
    {
        _mediator = mediator;
        _tokenService = tokenService;
        _context = context;
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="command">The command containing user registration data.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the registration.</returns>
    /// <response code="201">If the user was registered successfully.</response>
    /// <response code="400">If the registration data is invalid or a user with the same credentials already exists.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Authenticates a user and issues access and refresh tokens.
    /// </summary>
    /// <param name="query">The query containing user login credentials.</param>
    /// <returns>An <see cref="IActionResult"/> containing the access token on successful login, or an error.</returns>
    /// <response code="200">Returns the access token.</response>
    /// <response code="401">If authentication fails (e.g., invalid credentials).</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
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

    /// <summary>
    /// Refreshes an expired access token using a valid refresh token provided in a cookie.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a new access token on success, or an error.</returns>
    /// <response code="200">Returns a new access token.</response>
    /// <response code="401">If the refresh token is invalid or not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
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

    /// <summary>
    /// Revokes a refresh token, effectively logging out the user from all sessions using that token.
    /// Requires authorization.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the revocation.</returns>
    /// <response code="204">If the refresh token was successfully revoked (no content).</response>
    /// <response code="400">If the refresh token is not found or invalid.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="500">If an internal server error occurs.</response>
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
