using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.ProjectMember;
using ProjectManager.Application.Projects.Commands;
using ProjectManager.Application.Projects.Queries.GetAllProjects;
using ProjectManager.Application.Projects.Queries.GetProject;
using ProjectManager.Application.Projects.Queries.GetProjectDetails;
using ProjectManager.Application.TaskItems.Commands;

namespace ProjectManager.Controllers;

//[Authorize]
[ApiController]
[Route("api/projects")]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await mediator.Send(new GetAllProjectsQuery());

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await mediator.Send(new GetProjectQuery(id));

        return Ok(project);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetProjectDetails(Guid id)
    {
        var project = await mediator.Send(new GetProjectDetailsQuery(id));

        return Ok(project);
    }

    //[Authorize(Roles = Roles.Manager + "," + Roles.Admin)]
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

    //[Authorize(Roles = Roles.Manager + "," + Roles.Admin)]
    [HttpPost("{projectId}/projectMember")]
    public async Task<IActionResult> CreateProjectMember(Guid projectId, [FromBody] CreateProjectMemberRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateProjectMemberCommand(projectId, request.UserId), cancellationToken);

        return Created();
    }


    //[Authorize(Roles = Roles.Manager + "," + Roles.Admin)]
    [HttpPost("{projectId}/taskItems")]
    public async Task<IActionResult> CreateTaskItem(Guid projectId, [FromBody] CreateTaskItemRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateTaskItemCommand(projectId, request.Name), cancellationToken);

        return CreatedAtAction(nameof(GetTaskItem), new { projectId, taskId = id }, null);
    }

    //[Authorize(Roles = Roles.Manager + "," + Roles.Admin)]
    [HttpPost("{projectId}/tasksItems/{taskId}/assign/{memberId}")]
    public async Task<IActionResult> AssignTaskToMember(Guid projectId, Guid taskId, Guid memberId, CancellationToken cancellationToken)
    {
        await mediator.Send(new AssignTaskToProjectMemberCommand(projectId, taskId, memberId), cancellationToken);

        return NoContent();
    }
}