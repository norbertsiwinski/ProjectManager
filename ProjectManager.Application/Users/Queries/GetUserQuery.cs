using MediatR;
using ProjectManager.Application.Users.Dtos;

namespace ProjectManager.Application.Users.Queries;

public record GetUserQuery(Guid Id) : IRequest<UserResponse>;