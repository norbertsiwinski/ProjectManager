using MediatR;

namespace ProjectManager.Application.ProjectMember;

public record CreateProjectMemberCommand(Guid ProjectId, Guid UserId) : IRequest<Guid>;