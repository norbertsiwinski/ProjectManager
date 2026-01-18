using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.ProjectMember.Dtos;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.ProjectMember.Queries.GetProjectMember;

public class GetProjectMemberQueryHandler(IProjectRepository projectRepository, IUserRepository userRepository)
    : IRequestHandler<GetProjectMemberQuery, List<ProjectMemberResponse>>
{
    public async Task<List<ProjectMemberResponse>> Handle(GetProjectMemberQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException(nameof(Project));

        var projectMembers = project.Members.ToList();

        var users = await userRepository.GetByIdsAsync(
            projectMembers.Select(pm => pm.UserId).Distinct().ToList(), cancellationToken);

        var projectMembersResponse = projectMembers
            .Select(pm => {

                var user = users.FirstOrDefault(u => u.Id == pm.UserId) 
                    ?? throw new NotFoundException(nameof(User));

                return new ProjectMemberResponse(pm.Id.ToString(), user.Email.Value, user.Role.ToString());
            }
            ).ToList();

        return projectMembersResponse;
    }
}