namespace ProjectManager.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; init; } = id;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get;  set; }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}