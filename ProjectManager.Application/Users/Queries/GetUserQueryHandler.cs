using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Queries;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException(nameof(User));

        return new UserResponse(user.Email.Value, user.Role.ToString());
    }
}
