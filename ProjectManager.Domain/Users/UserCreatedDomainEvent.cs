using ProjectManager.Domain.Abstractions;

namespace ProjectManager.Domain.Users;

public sealed record UserCreatedDomainEvent(User User) : IDomainEvent;