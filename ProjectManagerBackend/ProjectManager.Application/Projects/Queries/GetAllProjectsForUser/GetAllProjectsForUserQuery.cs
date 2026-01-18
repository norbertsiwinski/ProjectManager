using MediatR;
using ProjectManager.Application.Projects.Dtos;

namespace ProjectManager.Application.Projects.Queries.GetAllProjectsForUser;

public record GetAllProjectsForUserQuery : IRequest<List<ProjectResponse>>;