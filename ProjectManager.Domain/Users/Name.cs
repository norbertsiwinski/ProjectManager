namespace ProjectManager.Domain.Users;

public sealed record Name
{
    public Name(string? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        Value = value;
    }

    
    public string Value { get; }
}