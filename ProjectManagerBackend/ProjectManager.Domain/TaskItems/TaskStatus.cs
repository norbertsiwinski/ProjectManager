using System.Text.Json.Serialization;

namespace ProjectManager.Domain.TaskItems;

public enum TaskStatus
{
    New,
    InProgress,
    Completed
}