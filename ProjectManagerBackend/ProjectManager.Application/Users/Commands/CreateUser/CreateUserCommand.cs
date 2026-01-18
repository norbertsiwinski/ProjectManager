using MediatR;

namespace ProjectManager.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequest<Guid>;