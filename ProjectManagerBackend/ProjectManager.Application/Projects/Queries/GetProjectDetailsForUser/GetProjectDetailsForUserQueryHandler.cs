using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.ProjectMember.Dtos;
using ProjectManager.Application.Projects.Dtos;
using ProjectManager.Application.TaskItems.Dtos;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Projects.Queries.GetProjectDetailsForUser;

public class GetProjectDetailsForUserQueryHandler(IProjectRepository projectRepository, IUserRepository userRepository, IUserContext userContext)
    : IRequestHandler<GetProjectDetailsForUserQuery, ProjectDetailsResponse>
{
    public async Task<ProjectDetailsResponse> Handle(GetProjectDetailsForUserQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));

        var user = userContext.GetCurrentUser()
                    ?? throw new NotFoundException(nameof(User));

        var taskItemResponses = project.Tasks
            .Where(t =>
            {
                var member = project.Members.FirstOrDefault(pm => pm.Id == t.ProjectMemberId);
                return member?.UserId.ToString() == user.Id;
            })
            .Select(t => new TaskItemResponse(
                t.Id.ToString(),
                t.Name.Value,
                t.Status.ToString(),
                user.Email))
        .ToList();

        var userIds = project.Members.Select(m => m.UserId).ToList();
        var users = await userRepository.GetByIdsAsync(userIds, cancellationToken);

        var memberResponses = users
            .Select(u => new ProjectMemberResponse(
                u.Id.ToString(),
                u.Email.Value,
                u.Role.ToString()))
            .ToList();

        return new ProjectDetailsResponse(project.Id, project.Name.Value, taskItemResponses, memberResponses);
    }
}