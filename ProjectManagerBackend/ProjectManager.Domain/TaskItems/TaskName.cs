namespace ProjectManager.Domain.TaskItems;

public sealed record TaskName
{
    public TaskName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
       
        Value = value;
    }

    public string Value { get; }
}