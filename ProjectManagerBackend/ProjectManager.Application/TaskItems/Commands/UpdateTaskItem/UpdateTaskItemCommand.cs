using MediatR;
using ProjectManager.Application.TaskItems.Dtos;

namespace ProjectManager.Application.TaskItems.Commands.UpdateTaskItem;

public record UpdateTaskItemCommand(Guid ProjectId, Guid TaskItemId, string? Name, string? Status, Guid? ProjectMemberId) 
    : IRequest<TaskItemResponse>;