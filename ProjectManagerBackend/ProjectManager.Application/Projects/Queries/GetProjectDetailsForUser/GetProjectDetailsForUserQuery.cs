using MediatR;
using ProjectManager.Application.Projects.Dtos;

namespace ProjectManager.Application.Projects.Queries.GetProjectDetailsForUser;

public record GetProjectDetailsForUserQuery(Guid Id) : IRequest<ProjectDetailsResponse>;