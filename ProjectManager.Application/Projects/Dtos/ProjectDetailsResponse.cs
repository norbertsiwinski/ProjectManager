using ProjectManager.Application.ProjectMember.Dtos;
using ProjectManager.Application.TaskItems.Dtos;

namespace ProjectManager.Application.Projects.Dtos;

public record ProjectDetailsResponse(Guid Id, string Name, List<TaskItemResponse> TaskItems, List<ProjectMemberResponse> ProjectMembers);