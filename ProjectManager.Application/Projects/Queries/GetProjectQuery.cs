using MediatR;
using ProjectManager.Application.Projects.Dtos;

namespace ProjectManager.Application.Projects.Queries;

public record GetProjectQuery(Guid Id) : IRequest<ProjectResponse>;