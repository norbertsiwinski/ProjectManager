using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.TaskItems.Dtos;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.TaskItems;
using System.ComponentModel.DataAnnotations;
using TaskStatus = ProjectManager.Domain.TaskItems.TaskStatus;

namespace ProjectManager.Application.TaskItems.Commands.UpdateTaskItem;

public class UpdateTaskItemCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskItemCommand, TaskItemResponse>
{
    public async Task<TaskItemResponse> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));

        var task = project.Tasks.FirstOrDefault(t => t.Id == request.TaskItemId)
                   ?? throw new NotFoundException(nameof(TaskItem));

        if (request.Name is not null) 
            project.RenameTask(task.Id, new TaskName(request.Name));

        if (request.Status is not null)
        {
            if (!Enum.TryParse(request.Status, out TaskStatus status))
            {
                throw new ValidationException($"Invalid task status: {request.Status}");
            }
            project.ChangeTaskStatus(task.Id, status);
        }

        if (request.ProjectMemberId is not null)
            project.AssignTaskToProjectMember(request.TaskItemId, request.ProjectMemberId.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new TaskItemResponse(task.Id.ToString(), task.Name.Value, task.Status.ToString(), task.Name.Value);
    }
}
