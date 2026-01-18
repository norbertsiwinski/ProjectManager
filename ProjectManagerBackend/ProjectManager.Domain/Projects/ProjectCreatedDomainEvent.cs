using ProjectManager.Domain.Abstractions;

namespace ProjectManager.Domain.Projects;

public class ProjectCreatedDomainEvent(Project project) : IDomainEvent
{
}