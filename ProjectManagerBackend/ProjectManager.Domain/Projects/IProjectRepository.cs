using ProjectManager.Domain.TaskItems;

namespace ProjectManager.Domain.Projects;

public interface IProjectRepository
{
    void Add(Project project);

    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken);

    Task<List<TaskItem>> GetTaskItemsForUserAsync(Guid userId, CancellationToken cancellationToken);
}