using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Users;
using System.Linq;

namespace ProjectManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public void Add(User user)
    {
        appDbContext.Users.Add(user);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<List<User>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return appDbContext.Users.Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }

    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken) => 
        appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
}