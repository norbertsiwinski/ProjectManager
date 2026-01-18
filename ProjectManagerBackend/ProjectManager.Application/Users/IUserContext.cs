namespace ProjectManager.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}