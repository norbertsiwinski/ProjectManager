using MediatR;

namespace ProjectManager.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string email, string password) : IRequest<string>;