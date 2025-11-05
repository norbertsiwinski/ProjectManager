using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Users;

namespace ProjectManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public void Add(User user)
    {
        appDbContext.Users.Add(user);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
}