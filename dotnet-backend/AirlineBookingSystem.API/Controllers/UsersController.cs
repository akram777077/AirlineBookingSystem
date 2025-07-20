using AirlineBookingSystem.Application.Features.Users.Commands.CreateUser;
using AirlineBookingSystem.Application.Features.Users.Commands.DeleteUser;
using AirlineBookingSystem.Application.Features.Users.Commands.UpdateUser;
using AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;
using AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;
using AirlineBookingSystem.Application.Features.Users.Queries.Search;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<UserDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchUsers([FromQuery] SearchUsersQuery query)
    {
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        var result = await sender.Send(new UpdateUserCommandWithId(
            id,
            command.Username,
            command.Email,
            command.FirstName,
            command.LastName,
            command.MidName,
            command.DateOfBirth,
            command.GenderId,
            command.Street,
            command.CityId,
            command.ZipCode,
            command.RoleId
        ));
        return this.ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    [HttpPatch("{id:int}/activate")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActivateUser(int id)
    {
        var command = new UpdateUserStatusCommand(id, true);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    [HttpPatch("{id:int}/deactivate")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeactivateUser(int id)
    {
        var command = new UpdateUserStatusCommand(id, false);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }
}