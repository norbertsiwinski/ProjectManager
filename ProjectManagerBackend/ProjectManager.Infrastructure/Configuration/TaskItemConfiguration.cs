using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.ProjectMembers;
using ProjectManager.Domain.TaskItems;

namespace ProjectManager.Infrastructure.Configuration;

internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasConversion(
                name => name.Value,
                value => new TaskName(value));

        builder.HasOne<ProjectMember>()
            .WithMany()
            .HasForeignKey(t => t.ProjectMemberId);
            
        builder.Property(t => t.Id).ValueGeneratedNever();
    }
}