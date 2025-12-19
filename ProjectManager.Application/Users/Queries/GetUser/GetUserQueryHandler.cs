using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.Users.Dtos;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException(nameof(User));

        var responseUser = mapper.Map<UserResponse>(user);

        return responseUser;
    }
}
