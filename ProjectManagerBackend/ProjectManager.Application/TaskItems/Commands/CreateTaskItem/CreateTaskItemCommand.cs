using MediatR;

namespace ProjectManager.Application.TaskItems.Commands.CreateTaskItem;

public record CreateTaskItemCommand(Guid ProjectId, string Name, Guid? ProjectMemberId) : IRequest<Guid>;