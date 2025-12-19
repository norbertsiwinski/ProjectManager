using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.ProjectMember.Commands;
using ProjectManager.Application.ProjectMember.Queries.GetProjectMember;
using ProjectManager.Application.Projects.Commands;
using ProjectManager.Application.Projects.Queries.GetAllProjects;
using ProjectManager.Application.Projects.Queries.GetAllProjectsForUser;
using ProjectManager.Application.Projects.Queries.GetProject;
using ProjectManager.Application.Projects.Queries.GetProjectDetails;
using ProjectManager.Application.Projects.Queries.GetProjectDetailsForUser;
using ProjectManager.Application.TaskItems.Commands.AssignTaskToProjectMember;
using ProjectManager.Application.TaskItems.Commands.CreateTaskItem;
using ProjectManager.Application.TaskItems.Commands.UpdateTaskItem;
using ProjectManager.Application.TaskItems.Queries;
using ProjectManager.Application.TaskItems.Queries.GetTaskitemsForUser;

namespace ProjectManager.Controllers;

[Authorize]
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

    [HttpGet("my")]
    public async Task<IActionResult> GetAllProjectsForUser()
    {
        var projects = await mediator.Send(new GetAllProjectsForUserQuery());

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

    [HttpGet("my/details/{id}")]
    public async Task<IActionResult> GetProjectDetailsForUser(Guid id)
    {
        var project = await mediator.Send(new GetProjectDetailsForUserQuery(id));

        return Ok(project);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateProjectCommand(request.Name), cancellationToken);

        return CreatedAtAction(nameof(GetProject), new {id}, null);
    }

    [HttpGet("my/taskItems")]
    public async Task<IActionResult> GetTasksItemForUser()
    {
        var taskItems = await mediator.Send(new GetTaskItemsForUserQuery());

        return Ok(taskItems);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{projectId}/projectMember")]
    public async Task<IActionResult> CreateProjectMember(Guid projectId, [FromBody] CreateProjectMemberRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateProjectMemberCommand(projectId, request.UserId), cancellationToken);

        return Created();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("projectMembers/{id}")]
    public async Task<IActionResult> GetProjectMembers(Guid id)
    {
        var project = await mediator.Send(new GetProjectMemberQuery(id));

        return Ok(project);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{projectId}/taskItems")]
    public async Task<IActionResult> CreateTaskItem(Guid projectId, [FromBody] CreateTaskItemRequest request, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(new CreateTaskItemCommand(projectId, request.Name, request.ProjectMemberId), cancellationToken);

        return CreatedAtAction(nameof(GetTasksItemForUser), new { projectId, taskId = id }, null);
    }

    [HttpPatch("{projectId}/taskItems/{taskItemId}")]
    public async Task<IActionResult> UpdateTaskItem(Guid projectId, Guid taskItemId, [FromBody] UpdateTaskItemRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateTaskItemCommand(projectId, taskItemId, request.Name, request.Status, request.ProjectMemberId), cancellationToken);

        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{projectId}/tasksItems/{taskId}/assign/{memberId}")]
    public async Task<IActionResult> AssignTaskToMember(Guid projectId, Guid taskId, Guid memberId, CancellationToken cancellationToken)
    {
        await mediator.Send(new AssignTaskToProjectMemberCommand(projectId, taskId, memberId), cancellationToken);

        return NoContent();
    }
}