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

[ApiController]
[Route("api/roles")]
public class RolesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await sender.Send(new GetAllRolesQuery());
        return this.ToActionResult(result);
    }

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
