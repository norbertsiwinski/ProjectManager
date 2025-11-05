using MediatR;

namespace ProjectManager.Application.Users.Queries;

public record GetUserQuery(Guid Id) : IRequest<UserResponse>;