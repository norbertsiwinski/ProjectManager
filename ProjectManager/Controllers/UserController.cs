using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Users.Commands.CreateUser;
using ProjectManager.Application.Users.Commands.LoginUser;
using ProjectManager.Application.Users.Queries;

namespace ProjectManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [Authorize]
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest loginUserRequest)
    {
        var token = await mediator.Send(new LoginUserCommand(loginUserRequest.Email, loginUserRequest.Password));

        return Ok(token);
    }

}