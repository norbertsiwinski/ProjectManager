using MediatR;
using ProjectManager.Application.TaskItems.Dtos;

namespace ProjectManager.Application.TaskItems.Queries.GetTaskitemsForUser;

public record GetTaskItemsForUserQuery : IRequest<List<TaskItemResponse>>;