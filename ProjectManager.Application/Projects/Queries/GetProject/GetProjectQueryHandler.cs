using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.Projects.Dtos;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Projects.Queries.GetProject;

public class GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper) 
    : IRequestHandler<GetProjectQuery, ProjectResponse?>
{
    public async Task<ProjectResponse?> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Project));

        var projectResponse = mapper.Map<ProjectResponse>(project);

        return projectResponse;
    }
}