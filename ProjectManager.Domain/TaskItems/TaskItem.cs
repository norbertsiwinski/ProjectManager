using ProjectManager.Domain.Abstractions;

namespace ProjectManager.Domain.TaskItems;

public class TaskItem : Entity
{
    private TaskItem(Guid id, Guid projectId, TaskName name) : base(id)
    {
        ProjectId = projectId;
        Name = name;
        Status = TaskStatus.New;
    }

    public TaskName Name { get; private set; }

    public TaskStatus Status { get; private set; }

    public Guid ProjectId { get; private set; }

    public Guid? ProjectMemberId { get; private set; }

    internal static TaskItem Create(Guid id, Guid projectId, TaskName name)
    {
        return new TaskItem(id, projectId, name);
    }

    internal void AssignTo(Guid memberId)
    {
        ProjectMemberId = memberId;
    }
}