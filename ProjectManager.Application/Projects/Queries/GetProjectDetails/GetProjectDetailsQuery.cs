using MediatR;
using ProjectManager.Application.Projects.Dtos;

namespace ProjectManager.Application.Projects.Queries.GetProjectDetails;

public record GetProjectDetailsQuery(Guid Id) : IRequest<ProjectDetailsResponse>;