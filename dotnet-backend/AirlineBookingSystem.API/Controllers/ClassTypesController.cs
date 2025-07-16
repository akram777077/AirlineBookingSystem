using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.API.Controllers;

[ApiController]
[Route("api/class-types")]
public class ClassTypesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClassTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllClassTypes()
    {
        var result = await sender.Send(new GetAllClassTypesQuery());
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClassTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetClassTypeById(int id)
    {
        var query = new GetClassTypeByIdQuery(id);
        var result = await sender.Send(query);
        return this.ToActionResult(result);
    }
}
