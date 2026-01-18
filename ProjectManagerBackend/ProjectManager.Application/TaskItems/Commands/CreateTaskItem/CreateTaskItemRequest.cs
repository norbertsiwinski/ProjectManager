namespace ProjectManager.Application.TaskItems.Commands.CreateTaskItem;

public record CreateTaskItemRequest(string Name, Guid? ProjectMemberId);