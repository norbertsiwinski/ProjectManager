namespace ProjectManager.Domain.Users;

public record PasswordHash
{
    public PasswordHash(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }
    public string Value { get; }
}