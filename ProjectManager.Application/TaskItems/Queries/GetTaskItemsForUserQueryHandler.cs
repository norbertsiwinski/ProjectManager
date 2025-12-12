using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.TaskItems.Dtos;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.TaskItems.Queries;

public class GetTaskItemsForUserQueryHandler(IProjectRepository projectRepository, IUserContext userContext) 
    : IRequestHandler<GetTaskItemsForUserQuery, List<TaskItemResponse>>
{
    public async Task<List<TaskItemResponse>> Handle(GetTaskItemsForUserQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser() 
                   ?? throw new NotFoundException(nameof(User));

        var userId = Guid.Parse(user.Id);

        var tasks = await projectRepository.GetTaskItemsForUserAsync(userId, cancellationToken);

        return tasks.Select(t => new TaskItemResponse(
            Name: t.Name.Value,
            Status: t.Status.ToString(),
            AssigneeName: user.Email,
            AssigneeId: user.Id
        )).ToList();
    }
}