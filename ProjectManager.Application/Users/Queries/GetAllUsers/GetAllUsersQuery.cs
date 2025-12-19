using MediatR;
using ProjectManager.Application.Users.Dtos;

namespace ProjectManager.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<UserResponse>>;
