using MediatR;
using ProjectManager.Application.Projects.Dtos;

namespace ProjectManager.Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<List<ProjectResponse>>;