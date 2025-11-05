using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Projects.Queries;

public class GetProjectQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectQuery, ProjectResponse?>
{
    public async Task<ProjectResponse?> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Project));

        return new ProjectResponse(project.Id, project.Name.Value);
    }
}