using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.ProjectMember.Commands;

public class CreateProjectMemberCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProjectMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                      ?? throw new NotFoundException(nameof(User));

        var projectMember = project.AddMember(user.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return projectMember.Id;
    }
}