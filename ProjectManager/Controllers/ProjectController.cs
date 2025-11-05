using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.ProjectMember;
using ProjectManager.Application.Projects.Commands;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.TaskItems.Commands;

namespace ProjectManager.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await mediator.Send(new GetProjectQuery(id));

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateProjectCommand(request.Name), cancellationToken);

        return CreatedAtAction(nameof(GetProject), new {id}, null);
    }

    [HttpGet("{projectId}/taskItems/{taskId}")]
    public async Task<IActionResult> GetTaskItem(Guid projectId, Guid taskId)
    {
        return Ok();
    }

    [HttpPost("{projectId}/projectMember")]
    public async Task<IActionResult> CreateProjectMember(Guid projectId, [FromBody] CreateProjectMemberRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateProjectMemberCommand(projectId, request.UserId), cancellationToken);

        return Created();
    }

    [HttpPost("{projectId}/taskItems")]
    public async Task<IActionResult> CreateTaskItem(Guid projectId, [FromBody] CreateTaskItemRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateTaskItemCommand(projectId, request.Name), cancellationToken);

        return CreatedAtAction(nameof(GetTaskItem), new { id }, null);
    }

    [HttpPost("{projectId}/tasksItems/{taskId}/assign/{memberId}")]
    public async Task<IActionResult> AssignTaskToMember(Guid projectId, Guid taskId, Guid memberId, CancellationToken cancellationToken)
    {
        await mediator.Send(new AssignTaskToProjectMemberCommand(projectId, taskId, memberId), cancellationToken);
        return NoContent();
    }
}