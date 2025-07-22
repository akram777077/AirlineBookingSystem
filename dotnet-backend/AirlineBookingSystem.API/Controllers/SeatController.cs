using AirlineBookingSystem.Application.Features.Seats.Commands.Create;
using AirlineBookingSystem.Application.Features.Seats.Commands.Update;
using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[Route("api/seats")]
[ApiController]
public class SeatController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSeat([FromBody] CreateSeatDto dto)
    {
        var result = await sender.Send(new CreateSeatCommand(dto));
        return result.IsSuccess ? CreatedAtAction(nameof(GetSeatById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSeat(int id, [FromBody] UpdateSeatDto dto)
    {
        var result = await sender.Send(new UpdateSeatCommand(id, dto));
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SeatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSeatById(int id)
    {
        var result = await sender.Send(new GetSeatByIdQuery(id));
        return this.ToActionResult(result);
    }
}