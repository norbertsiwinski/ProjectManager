using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Infrastructure.Configuration;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasConversion(
                name => name.Value,
                value => new ProjectName(value));

        builder.HasMany(p => p.Tasks)
               .WithOne()
               .HasForeignKey(p => p.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Members)
               .WithOne()
               .HasForeignKey(pm => pm.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}