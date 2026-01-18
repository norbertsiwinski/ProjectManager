using AutoMapper;
using MediatR;
using ProjectManager.Application.Exceptions;
using ProjectManager.Application.Users.Dtos;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(cancellationToken) 
                    ?? throw new NotFoundException(nameof(User));

        var userResponse = mapper.Map<List<UserResponse>>(users);

        return userResponse;
    }
}
