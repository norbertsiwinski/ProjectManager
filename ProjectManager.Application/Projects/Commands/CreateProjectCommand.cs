using MediatR;

namespace ProjectManager.Application.Projects.Commands;

public record CreateProjectCommand(string Name) : IRequest<Guid>;