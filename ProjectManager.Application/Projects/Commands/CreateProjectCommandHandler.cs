using MediatR;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Projects.Commands;

public class CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateProjectCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = Project.Create(new ProjectName(request.Name));
        projectRepository.Add(project);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}