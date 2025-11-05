using MediatR;

namespace ProjectManager.Application.Projects.Queries;

public record GetProjectQuery(Guid Id) : IRequest<ProjectResponse>;