namespace ProjectManager.Domain.Projects;

public sealed record ProjectName
{
    public ProjectName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
}