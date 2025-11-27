using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Coink.Application.commands.CreateUser;
using Coink.Application.Users.Dtos;
using Coink.Application.Users.Queries.GetUserById;

namespace Coink.Presentation.controller;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequestDto requestDto)
    {
        var command = _mapper.Map<CreateUserCommand>(requestDto);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Obtiene un usuario por Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
