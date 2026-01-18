using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Abstractions.Security;

public interface ITokenProvider
{
    string Create(User user);
}