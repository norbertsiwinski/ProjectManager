using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.Projects.Dtos;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Projects.Queries.GetAllProjectsForUser;

public class GetAllProjectsForUserQueryHandler(IProjectRepository projectRepository, IUserContext userContext, IMapper mapper)
    : IRequestHandler<GetAllProjectsForUserQuery, List<ProjectResponse>>
{
    public async Task<List<ProjectResponse>> Handle(GetAllProjectsForUserQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync(cancellationToken)
                       ?? throw new NotFoundException(nameof(Project));

        var user = userContext.GetCurrentUser()
                   ?? throw new NotFoundException(nameof(User));

        projects = projects.Where(p => p.Members.Any(m => m.UserId.ToString() == user.Id)).ToList();

        var projectsResponse = mapper.Map<List<ProjectResponse>>(projects);

        return projectsResponse;
    }
}