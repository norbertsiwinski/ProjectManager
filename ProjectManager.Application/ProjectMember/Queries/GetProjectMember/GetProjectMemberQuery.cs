using MediatR;
using ProjectManager.Application.ProjectMember.Dtos;

namespace ProjectManager.Application.ProjectMember.Queries.GetProjectMember;

public record GetProjectMemberQuery(Guid Id) : IRequest<List<ProjectMemberResponse>>;