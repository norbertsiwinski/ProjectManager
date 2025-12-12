using MediatR;
using ProjectManager.Application.TaskItems.Dtos;

namespace ProjectManager.Application.TaskItems.Queries;

public record GetTaskItemsForUserQuery : IRequest<List<TaskItemResponse>>;