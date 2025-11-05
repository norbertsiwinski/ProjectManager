using Microsoft.EntityFrameworkCore;
using ProjectManager.Application;
using ProjectManager.Domain.Abstractions;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.Users;

namespace ProjectManager.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Project> Projects => Set<Project>();

    public DbSet<User> Users => Set<User>();

    public void ApplyAudit()
    {
        foreach (var e in ChangeTracker.Entries<Entity>())
        {
            switch (e.State)
            {
                case EntityState.Added:
                    e.Entity.CreatedAt = DateTime.UtcNow;
                    e.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    e.Property(x => x.CreatedAt).IsModified = false;
                    e.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();
        return base.SaveChangesAsync(cancellationToken);
    }
}