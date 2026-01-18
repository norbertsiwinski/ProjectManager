using ProjectManager.Domain.Abstractions;
using ProjectManager.Domain.TaskItems;

namespace ProjectManager.Domain.ProjectMembers;

public class ProjectMember : Entity
{
    private ProjectMember(Guid id, Guid projectId, Guid userId) : base(id)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public Guid ProjectId { get; private set; }

    public static ProjectMember Create(Guid Id, Guid projectId, Guid userId)
    {
        return new ProjectMember(Id, projectId, userId);
    }
}