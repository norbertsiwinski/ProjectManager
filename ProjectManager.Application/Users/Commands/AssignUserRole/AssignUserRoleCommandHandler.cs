using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                   ?? throw new NotFoundException(nameof(User));

        user.AssignRole(request.Role);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}