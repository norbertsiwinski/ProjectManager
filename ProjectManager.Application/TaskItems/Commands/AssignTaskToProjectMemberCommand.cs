using MediatR;

namespace ProjectManager.Application.TaskItems.Commands;

public record AssignTaskToProjectMemberCommand(Guid ProjectId, Guid TaskId, Guid ProjectMemberId) : IRequest;