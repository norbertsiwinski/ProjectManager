using ProjectManager.Domain.Abstractions;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.ProjectMembers;
using ProjectManager.Domain.TaskItems;
using TaskStatus = ProjectManager.Domain.TaskItems.TaskStatus;

namespace ProjectManager.Domain.Projects;

public class Project : AggregateRoot
{
    private readonly List<ProjectMember> members = new();
    private readonly List<TaskItem> tasks = new();

    private Project(Guid id, ProjectName name) : base(id)
    {
        Name = name;
    }

    public ProjectName Name { get; private set; }

    public IReadOnlyCollection<ProjectMember> Members => members;

    public IReadOnlyCollection<TaskItem> Tasks => tasks;

    public static Project Create(ProjectName name)
    {
        var project = new Project(Guid.NewGuid(), name);
        project.Raise(new ProjectCreatedDomainEvent(project));

        return project;
    }

    public TaskItem AddTask(TaskName name)
    {
        var task = TaskItem.Create(Guid.NewGuid(), Id, name);
        tasks.Add(task);

        return task;
    }

    public ProjectMember AddMember(Guid userId)
    {
        if (members.Any(m => m.UserId == userId))
            throw new DomainException($"User {userId} already assigned to project.");

        var projectMember = ProjectMember.Create(Guid.NewGuid(), Id, userId);
        members.Add(projectMember);

        return projectMember;
    }

    public void RenameTask(Guid taskId, TaskName newName)
    {
        var task = tasks.FirstOrDefault(t => t.Id == taskId)
                   ?? throw new DomainException($"Task {taskId} does not belong to project {Id}.");

        task.Rename(newName);
    }

    public void ChangeTaskStatus(Guid taskId, TaskStatus newStatus)
    {
        var task = tasks.FirstOrDefault(t => t.Id == taskId)
                   ?? throw new DomainException($"Task {taskId} does not belong to project {Id}.");

        task.ChangeStatus(newStatus);
    }
    public void AssignTaskToProjectMember(Guid taskId, Guid projectMemberId)
    {
        var member = members.FirstOrDefault(m => m.Id == projectMemberId)
                     ?? throw new DomainException($"Project member {projectMemberId} does not belong to project {Id}.");

        var task = tasks.FirstOrDefault(m => m.Id == taskId)
                   ?? throw new DomainException($"Task {taskId} does not belong to project {Id}.");

        task.AssignTo(member.Id);
    }
}