using MediatR;
using ProjectManager.Application.Users.Dtos;

namespace ProjectManager.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : IRequest<UserResponse>;