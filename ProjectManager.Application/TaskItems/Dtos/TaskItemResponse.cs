namespace ProjectManager.Application.TaskItems.Dtos;

public record TaskItemResponse(string Name, string Status, string? AssigneeName, string? AssigneeId);