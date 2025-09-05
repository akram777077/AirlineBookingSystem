using AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;
using AirlineBookingSystem.Application.Features.Roles.Queries.GetById;
using AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;
using AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;
using AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing role and role-permission related operations.
/// </summary>
[ApiController]
[Route("api/roles")]
public class RolesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Retrieves all available roles.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="RoleDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of all roles.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await sender.Send(new GetAllRolesQuery());
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific role by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="RoleDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the role details.</response>
    /// <response code="404">If a role with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var query = new GetRoleByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves all permissions assigned to a specific role.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <returns>An <see cref="IActionResult"/> containing a list of <see cref="PermissionDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns a list of permissions for the specified role.</response>
    /// <response code="404">If the role is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{roleId:int}/permissions")]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRolePermissions(int roleId)
    {
        var query = new GetRolePermissionsQuery(roleId);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Assigns a list of permissions to a specific role.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="permissionIds">A list of unique identifiers of permissions to assign.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If permissions were assigned successfully (no content).</response>
    /// <response code="404">If the role or any of the permissions are not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost("{roleId:int}/permissions")]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignPermissionsToRole(int roleId, [FromBody] List<int> permissionIds)
    {
        var command = new AssignPermissionsToRoleCommand(roleId, permissionIds);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Removes a specific permission from a role.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="permissionId">The unique identifier of the permission to remove.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the permission was removed successfully (no content).</response>
    /// <response code="404">If the role or permission is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete("{roleId:int}/permissions/{permissionId:int}")]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemovePermissionFromRole(int roleId, int permissionId)
    {
        var command = new RemovePermissionFromRoleCommand(roleId, permissionId);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }
}
