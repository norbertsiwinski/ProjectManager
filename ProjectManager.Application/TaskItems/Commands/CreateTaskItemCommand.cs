using MediatR;

namespace ProjectManager.Application.TaskItems.Commands;

public record CreateTaskItemCommand(Guid projectId, string Name) : IRequest<Guid>;