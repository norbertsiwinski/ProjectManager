using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Users.Commands;
using ProjectManager.Application.Users.Queries;

namespace ProjectManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await mediator.Send(new GetUserQuery(id));

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        var id = await mediator.Send(new CreateUserCommand(createUserRequest.Email, createUserRequest.Password));

        return CreatedAtAction(nameof(GetUser), new {id}, null);
    }
}