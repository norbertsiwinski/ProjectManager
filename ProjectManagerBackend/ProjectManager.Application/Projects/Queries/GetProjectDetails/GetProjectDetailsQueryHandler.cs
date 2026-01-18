using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.ProjectMember.Dtos;
using ProjectManager.Application.Projects.Dtos;
using ProjectManager.Application.TaskItems.Dtos;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Projects.Queries.GetProjectDetails;

public class GetProjectDetailsQueryHandler(IProjectRepository projectRepository, IUserRepository userRepository)
    : IRequestHandler<GetProjectDetailsQuery, ProjectDetailsResponse>
{
    public async Task<ProjectDetailsResponse> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new NotFoundException(nameof(Project));

        var userIds = project.Members.Select(pm => pm.UserId).Distinct().ToList();

        var users = await userRepository.GetByIdsAsync(userIds, cancellationToken);

        var taskItemResponses = project.Tasks
            .Select(task =>
            {
                var member = project.Members.FirstOrDefault(pm => pm.Id == task.ProjectMemberId);

                var user = member is null ? null
                    : users.FirstOrDefault(u => u.Id == member.UserId);

                return new TaskItemResponse(
                    task.Id.ToString(),
                    task.Name.Value,
                    task.Status.ToString(),
                    user?.Email.Value
                    );
            })
            .ToList();

        var memberResponses = project.Members
            .Select(pm =>
            {
                var user = users.FirstOrDefault(u => u.Id == pm.UserId)
                           ?? throw new NotFoundException(nameof(User));

                return new ProjectMemberResponse(
                    pm.Id.ToString(),
                    user.Email.Value,
                    user.Role.ToString());

            }).ToList();

        return new ProjectDetailsResponse(project.Id, project.Name.Value, taskItemResponses, memberResponses);
    }
}