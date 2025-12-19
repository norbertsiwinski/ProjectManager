using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.TaskItems.Commands.AssignTaskToProjectMember;

public class AssignTaskToProjectMemberCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<AssignTaskToProjectMemberCommand>
{
    public async Task Handle(AssignTaskToProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Project));

        project.AssignTaskToProjectMember(request.TaskId, request.ProjectMemberId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}