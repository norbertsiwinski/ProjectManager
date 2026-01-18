namespace ProjectManager.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles);