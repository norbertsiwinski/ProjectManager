
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.ProjectMembers;

namespace ProjectManager.Infrastructure.Configuration;

internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.ToTable("ProjectMembers");

        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.HasKey(p => p.Id);
    }
}