using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.TaskItems;

namespace ProjectManager.Application.TaskItems.Commands.CreateTaskItem;

public class CreateTaskItemCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateTaskItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));
        
        var taskItem = project.AddTask(new TaskName(request.Name));

        if (request.ProjectMemberId is not null)
        {
            project.AssignTaskToProjectMember(taskItem.Id, request.ProjectMemberId.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return taskItem.Id;
    }
}