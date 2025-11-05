using MediatR;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Commands;

public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //TO DO: HASH PASSWORD
        var user = User.Create(new Email(request.Email), request.Password);

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}