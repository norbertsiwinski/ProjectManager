using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Infrastructure.Repositories;

internal class ProjectRepository(AppDbContext context) : IProjectRepository
{
    public void Add(Project project) => context.Projects.Add(project);

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Projects
            .Include(p => p.Members)
            .Include(p => p.Tasks)
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Projects
            .Include(p => p.Members)
            .Include(p => p.Tasks)
            .AsTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        
    }
}
