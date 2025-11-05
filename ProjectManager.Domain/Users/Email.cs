namespace ProjectManager.Domain.Users;

public sealed record Email
{
    public Email(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
}