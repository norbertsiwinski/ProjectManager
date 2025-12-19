namespace ProjectManager.Application.TaskItems.Commands.UpdateTaskItem;

public record UpdateTaskItemRequest(string? Name, string? Status, Guid? ProjectMemberId);