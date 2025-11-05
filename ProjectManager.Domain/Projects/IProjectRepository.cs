namespace ProjectManager.Domain.Projects;

public interface IProjectRepository
{
    void Add(Project project);

    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}