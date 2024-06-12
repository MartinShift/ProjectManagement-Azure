using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ProjectContext context) : base(context)
    {
    }

    public async Task<User> GetByLoginAsync(string login)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(x => x.UserName == login);
    }
}
