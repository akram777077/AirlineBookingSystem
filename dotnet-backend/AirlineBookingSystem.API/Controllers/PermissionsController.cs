using AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;
using AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing permission-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(_permissionRoutes.BaseRoute)]
[EnableRateLimiting("fixed")]
public class PermissionsController(ISender sender) : ControllerBase
{
    private readonly PermissionRoutes _permissionRoutes = new();
{
    /// <summary>
    /// Retrieves all available permissions.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="PermissionDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all permissions.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var result = await sender.Send(new GetAllPermissionsQuery());
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific permission by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the permission.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="PermissionDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the permission details.</response>
    /// <response code="404">If a permission with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(_permissionRoutes.GetByIdRoute)]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPermissionById(int id)
    {
        var query = new GetPermissionByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
