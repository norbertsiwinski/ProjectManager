using MediatR;

namespace ProjectManager.Application.Users.Commands.AssignUserRole;

public record AssignUserRoleCommand(Guid UserId, string Role) : IRequest;
