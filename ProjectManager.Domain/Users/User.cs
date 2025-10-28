using ProjectManager.Domain.Abstractions;

namespace ProjectManager.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name) : base(id)
    {
        Name = name; ;
    }

    public Name Name { get; private set; } 

    public static User Create(Name name)
    {
        var user = new User(Guid.NewGuid(), name);
        user.Raise(new UserCreatedDomainEvent(user));

        return user;
    }
}