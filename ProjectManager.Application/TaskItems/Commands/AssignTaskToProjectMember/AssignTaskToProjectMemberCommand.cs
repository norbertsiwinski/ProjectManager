using MediatR;

namespace ProjectManager.Application.TaskItems.Commands.AssignTaskToProjectMember;

public record AssignTaskToProjectMemberCommand(Guid ProjectId, Guid TaskId, Guid ProjectMemberId) : IRequest;