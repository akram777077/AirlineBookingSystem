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

using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirlineBookingSystem.API.Routes;
using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers;

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route(UserRoutes.Base)]
[EnableRateLimiting("fixed")]
public class UsersController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Searches for users based on various criteria and provides paginated results.
    /// </summary>
    /// <param name="query">An object containing search parameters for users, including pagination details.</param>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="PagedResult{List{UserDto}}"/> if successful, or an error.</returns>
    /// <response code="200">Returns a paginated list of users matching the criteria.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<List<UserDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchUsers([FromQuery] SearchUsersQuery query)
    {
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a specific user by their ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>An <see cref="IActionResult"/> containing <see cref="UserDto"/> if successful, or an error.</returns>
    /// <response code="200">Returns the user details.</response>
    /// <response code="404">If a user with the specified ID is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet(UserRoutes.GetById)]
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

    /// <summary>
    /// Creates a new user record in the system.
    /// </summary>
    /// <param name="command">The command containing data for the new user.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="201">If the user was created successfully.</response>
    /// <response code="400">If the provided user data is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Updates an existing user record identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="command">The command containing updated data for the user.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the user was updated successfully (no content).</response>
    /// <response code="400">If the provided user data is invalid.</response>
    /// <response code="404">If a user with the specified ID is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut(UserRoutes.GetById)]
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

    /// <summary>
    /// Deletes a user record from the system by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the user was deleted successfully (no content).</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If a user with the specified ID is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete(UserRoutes.GetById)]
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

    /// <summary>
    /// Activates a user account identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to activate.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the user was activated successfully (no content).</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If a user with the specified ID is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPatch(UserRoutes.Activate)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActivateUser(int id)
    {
        var command = new ActivateUserCommand(id);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Deactivates a user account identified by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to deactivate.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the operation.</returns>
    /// <response code="204">If the user was deactivated successfully (no content).</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If a user with the specified ID is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPatch(UserRoutes.Deactivate)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeactivateUser(int id)
    {
        var command = new DeactivateUserCommand(id);
        var result = await sender.Send(command);
        return this.ToActionResult(result);
    }
}