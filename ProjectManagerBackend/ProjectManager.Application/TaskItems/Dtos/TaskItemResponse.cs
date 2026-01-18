namespace ProjectManager.Application.TaskItems.Dtos;

public record TaskItemResponse(string Id, string Name, string Status, string? AssigneeName);