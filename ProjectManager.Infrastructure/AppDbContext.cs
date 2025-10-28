using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions;

namespace ProjectManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            foreach (var e in ChangeTracker.Entries<Entity>())
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        e.Entity.CreatedAt = DateTime.Now;
                        e.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        e.Property(x => x.CreatedAt).IsModified = false;
                        e.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
