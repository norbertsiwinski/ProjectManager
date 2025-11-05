using ProjectManager.Domain.Abstractions;
using ProjectManager.Domain.ProjectMembers;

namespace ProjectManager.Domain.Users;

public class User : AggregateRoot
{
    private readonly List<ProjectMember> memberships = new();

    private User(Guid id, Email email, string passwordHash) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = Role.Developer;
    }

    public Email Email { get; private set; }

    public IReadOnlyCollection<ProjectMember> Memberships => memberships;

    public string PasswordHash { get; private set; } 

    public Role Role { get; private set; } 

    public static User Create(Email email, string passwordHash)
    {
        var user = new User(Guid.NewGuid(), email, passwordHash);
        user.Raise(new UserCreatedDomainEvent(user));

        return user;
    }
}