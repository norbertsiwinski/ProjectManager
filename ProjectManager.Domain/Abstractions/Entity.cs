namespace ProjectManager.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    private readonly List<IDomainEvent> _domainEvents = new();

   public Guid Id { get; init; } 

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get;  set; }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}