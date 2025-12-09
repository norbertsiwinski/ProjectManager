using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.Projects.Dtos;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper) 
    : IRequestHandler<GetAllProjectsQuery, List<ProjectResponse>>
{
    public async Task<List<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync(cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));

        var projectsResponse = mapper.Map<List<ProjectResponse>>(projects);

        return projectsResponse;
    }
}