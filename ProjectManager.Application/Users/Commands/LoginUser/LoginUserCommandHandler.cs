using MediatR;
using ProjectManager.Application.Abstractions.Security;
using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken)
                   ?? throw new NotFoundException(nameof(User));

        bool verified = passwordHasher.VerifyHashedPassword(user.PasswordHash.Value, request.password);

        if (!verified)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = tokenProvider.Create(user);

        return token;
    }
}