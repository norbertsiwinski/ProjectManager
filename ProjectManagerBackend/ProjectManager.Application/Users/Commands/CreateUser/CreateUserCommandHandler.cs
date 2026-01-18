using MediatR;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Abstractions.Security;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //TO DO: HASH PASSWORD
        var hash = passwordHasher.HashPassword(request.Password);
        var passwordHash = new PasswordHash(hash);

        var user = User.Create(new Email(request.Email), passwordHash);

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}