using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Users;

namespace ProjectManager.Infrastructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email).HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash).HasConversion(
                passwordHash => passwordHash.Value,
                value => new PasswordHash(value))
            .HasMaxLength(255);

        builder.HasMany(u => u.Memberships)
            .WithOne()
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}