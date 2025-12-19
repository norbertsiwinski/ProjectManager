using MediatR;

namespace ProjectManager.Application.ProjectMember.Commands;

public record CreateProjectMemberCommand(Guid ProjectId, Guid UserId) : IRequest<Guid>;